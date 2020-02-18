using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Peer;

struct PlayerObject {
    public uint id;
    public string name;
    public Vector2 pos;
    public Quaternion rot;
    public bool useWeapon;

    public PlayerObject(uint _id) {
        id = _id;
        name = "";
        pos = new Vector2(0, 0);
        rot = new Quaternion(0, 0, 1, 0);
        useWeapon = false;
    }
}


public class ServerScript : MonoBehaviour
{
    public Peer peer { get; private set; }
    private NetworkReader m_NetworkReader;
    private NetworkWriter m_NetworkWriter;
    public static ServerScript Instance;

    public WeaponManager weaponManager;
    public Rigidbody2D beam;

    private Dictionary<ulong, PlayerObject> clients = new Dictionary<ulong, PlayerObject>();
    //private Dictionary<ulong, ShipObject> clients = new Dictionary<ulong, ShipObject>();
    //private List<Rigidbody2D> laserBeams = new List<Rigidbody2D>();
    //private List<ShipObject> AIShips = new List<ShipObject>();
    //private float AISpeed = 0.5f;
    private uint clientID;

    private void Awake() {
        Instance = this;
    }

    public void Init(int _port) {
        StartServer("192.168.1.100", _port, 4);
    }

    public void StopServer() {
        if (peer != null) {
            peer.Close();
            Debug.LogError("[Server] Shutting down...");
        }
    }

    private void OnDestroy() {
        StopServer();
    }

    public int MaxConnections { get; private set; } = -1;

    public bool StartServer(string ip, int port, int maxConnections) {
        if (peer == null) {
            peer = new Peer();
            peer = CreateServer(ip, port, maxConnections);

            if (peer != null) {
                MaxConnections = maxConnections;
                Debug.Log("[Server] Server initialized on port " + port);

                Debug.Log("-------------------------------------------------");
                Debug.Log("|     Max connections: " + maxConnections);
                Debug.Log("|     Max FPS: " + (Application.targetFrameRate != -1 ? Application.targetFrameRate : 1000) + "(" + Time.deltaTime.ToString("f3") + " ms)");
                Debug.Log("|     Tickrate: " + (1 / Time.fixedDeltaTime) + "(" + Time.fixedDeltaTime.ToString("f3") + " ms)");
                Debug.Log("-------------------------------------------------");

                m_NetworkReader = new NetworkReader(peer);
                m_NetworkWriter = new NetworkWriter(peer);

                return true;
            }
            else {
                Debug.LogError("[Server] Starting failed...");
                return false;
            }
        }
        else {
            return true;
        }
    }

    private void FixedUpdate() {
        if (peer != null) {
            while (peer.Receive()) {
                m_NetworkReader.StartReading();
                byte b = m_NetworkReader.ReadByte();

                OnReceivedPacket(b);
            }
        }
    }


    private void OnReceivedPacket(byte packet_id) {
        bool IsInternalNetworkPackets = packet_id <= 134;

        if (IsInternalNetworkPackets) {
            if (packet_id == (byte)RakNet_Packets_ID.NEW_INCOMING_CONNECTION) {
                OnConnected();
            }

            if (packet_id == (byte)RakNet_Packets_ID.CONNECTION_LOST || packet_id == (byte)RakNet_Packets_ID.DISCONNECTION_NOTIFICATION) {
                Connection conn = FindConnection(peer.incomingGUID);

                if (conn != null) {
                    OnDisconnected(FindConnection(peer.incomingGUID));

                }
            }
        }
        else {
            switch (packet_id) {
                case (byte)Packets_ID.CL_INFO:
                    OnReceivedClientNetInfo(peer.incomingGUID);
                    break;
                case (byte)Packets_ID.ID_INITIALSTATS:
                    OnReceivedClientInitialStats(peer.incomingGUID);
                    break;
                case (byte)Packets_ID.ID_MOVEMENT:
                    OnReceivedClientMovementData(peer.incomingGUID);
                    break;
                case (byte)Packets_ID.NET_CHAT:
                    OnReceivedChatData(peer.incomingGUID);
                    break;
            }
        }
    }



    #region Connections
    public List<Connection> connections = new List<Connection>();
    private Dictionary<ulong, Connection> connectionByGUID = new Dictionary<ulong, Connection>();

    public List<ulong> guids = new List<ulong>();

    public Connection FindConnection(ulong guid) {
        if (connectionByGUID.TryGetValue(guid, out Connection value)) {
            return value;
        }
        return null;
    }

    private void AddConnection(Connection connection) {
        connections.Add(connection);
        connectionByGUID.Add(connection.guid, connection);
        guids.Add(connection.guid);
    }

    private void RemoveConnection(Connection connection) {
        clients.Remove(connection.guid);
        connectionByGUID.Remove(connection.guid);
        connections.Remove(connection);
        guids.Remove(connection.guid);
    }

    public static Connection[] Connections {
        get {
            return Instance.connections.ToArray();
        }
    }

    public static Connection GetByID(int id) {
        if (Connections.Length > 0) {
            return Connections[id];
        }

        return null;
    }

    public static Connection GetByIP(string ip) {
        foreach (Connection c in Connections) {
            if (c.ipaddress == ip) {
                return c;
            }
        }

        return null;
    }

    public static Connection GetByName(string name) {
        foreach (Connection c in Connections) {
            if (c.Info.name == name) {
                return c;
            }
        }

        return null;
    }

    public static Connection GetByHWID(string hwid) {
        foreach (Connection c in Connections) {
            if (c.Info.client_hwid == hwid) {
                return c;
            }
        }

        return null;
    }

    #endregion

    #region Events
    private void OnConnected() {
        Connection connection = new Connection(peer, peer.incomingGUID, connections.Count);

        AddConnection(connection);

        Debug.Log("[Server] Connection established " + connection.ipaddress);
        //peer.SendData(guid, Peer.Reliability.Reliable, 0, m_NetworkWriter);

        peer.SendPacket(connection, Packets_ID.CL_INFO, m_NetworkWriter);
    }

    private void OnDisconnected(Connection connection) {
        if (connection != null) {
            try {
                Debug.LogError("[Server] " + connection.Info.name + " disconnected [IP: " + connection.ipaddress + "]");

                RemoveConnection(connection);
            }
            catch {
                Debug.LogError("[Server] Unassigned connection destroyed!");
            }
        }
    }

    //private void SendAIPosition() {
    //    if (m_NetworkWriter.StartWritting()) {
    //        m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_AISHIPSMOVEMENT);
    //        m_NetworkWriter.Write(AIShips.Count);

    //        foreach (ShipObject tempObj in AIShips) {
    //            m_NetworkWriter.Write(tempObj.id);
    //            m_NetworkWriter.Write(tempObj.m_x);
    //            m_NetworkWriter.Write(tempObj.m_y);
    //            m_NetworkWriter.Write(tempObj.rotation);
    //        }
    //        SendToAll(m_NetworkWriter);
    //    }
    //}

    //private void SendPowerUpData(GameObject pu) {
    //    if (m_NetworkWriter.StartWritting()) {
    //        m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_POWERUP);
    //        m_NetworkWriter.Write(pu.transform.position);
    //        SendToAll(m_NetworkWriter);
    //    }
    //}

    private void OnReceivedChatData(ulong guid) {

        string chatString = m_NetworkReader.ReadString();

        if (m_NetworkWriter.StartWriting()) {
            m_NetworkWriter.WritePacketID((byte)Packets_ID.NET_CHAT);
            m_NetworkWriter.Write(chatString);
            SendToAll(m_NetworkWriter);
        }
    }

    private void OnReceivedClientMovementData(ulong guid) {
        PlayerObject tempObj = clients[guid];
        tempObj.pos = m_NetworkReader.ReadVector2();
        tempObj.rot = m_NetworkReader.ReadQuaternion();
        tempObj.useWeapon = m_NetworkReader.ReadBoolean();
        //tempObj.m_x = m_NetworkReader.ReadFloat();
        //tempObj.m_y = m_NetworkReader.ReadFloat();
        //tempObj.rotation = m_NetworkReader.ReadFloat();
        //tempObj.velocityX = m_NetworkReader.ReadFloat();
        //tempObj.velocityY = m_NetworkReader.ReadFloat();
        //tempObj.rotationVelocity = m_NetworkReader.ReadFloat();
        //tempObj.isShooting = m_NetworkReader.ReadBoolean();

        if (tempObj.useWeapon) {
            //Quaternion qRotation = Quaternion.Euler(0, 0, tempObj.rotation);

            //Rigidbody2D p = new Rigidbody2D();
            //p.position = tempObj.pos;
            //p.transform.rotation = tempObj.rot;
            //Rigidbody2D p = Instantiate(playerBeam, new Vector2(tempObj.m_x, tempObj.m_y), qRotation);
            //p.velocity = p.gameObject.transform.up * 5;

            //Rigidbody2D p = Instantiate(beam, tempObj.pos, tempObj.rot);
            
            SendWeaponData(tempObj.pos, tempObj.rot);
        }


        clients[guid] = tempObj;

        if (m_NetworkWriter.StartWriting()) {
            m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_MOVEMENT);
            m_NetworkWriter.Write(tempObj.id);
            m_NetworkWriter.Write(tempObj.pos);
            m_NetworkWriter.Write(tempObj.rot);
            SendToOthers(m_NetworkWriter, guid);
        }

        //if (m_NetworkWriter.StartWritting()) {
        //    m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_MOVEMENT);
        //    m_NetworkWriter.Write(tempObj.id);
        //    m_NetworkWriter.Write(tempObj.m_x);
        //    m_NetworkWriter.Write(tempObj.m_y);
        //    m_NetworkWriter.Write(tempObj.rotation);
        //    m_NetworkWriter.Write(tempObj.velocityX);
        //    m_NetworkWriter.Write(tempObj.velocityY);
        //    m_NetworkWriter.Write(tempObj.rotationVelocity);
        //    m_NetworkWriter.Write(tempObj.isShooting);

        //    //SendToAll(guid, m_NetworkWriter, true);
        //    SendToOthers(m_NetworkWriter, guid);
        //}
    }

    private void OnReceivedClientInitialStats(ulong guid) {
        PlayerObject tempObj = clients[guid];
        tempObj.name = m_NetworkReader.ReadString();
        tempObj.pos = m_NetworkReader.ReadVector2();
        tempObj.rot = m_NetworkReader.ReadQuaternion();

        clients[guid] = tempObj;

        if (m_NetworkWriter.StartWriting()) {
            m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_NEWCLIENT);
            m_NetworkWriter.Write(tempObj.id);
            m_NetworkWriter.Write(tempObj.name);
            m_NetworkWriter.Write(tempObj.pos);
            m_NetworkWriter.Write(tempObj.rot);

            //m_NetworkWriter.Write(tempObj.name);
            //m_NetworkWriter.Write(tempObj.m_x);
            //m_NetworkWriter.Write(tempObj.m_y);
            //m_NetworkWriter.Write(tempObj.shipNum);

            SendToOthers(m_NetworkWriter, guid);
            //SendToAll(guid, m_NetworkWriter, true);
            //peer.SendBroadcast(Peer.Priority.Immediate, Peer.Reliability.Reliable, 0);
            //SendToAll(m_NetworkWriter);
        }
    }

    private void SendWeaponData(Vector2 pos, Quaternion rot) {
        if (m_NetworkWriter.StartWriting()) {
            m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_WEAPONDATA);

            m_NetworkWriter.Write(pos);
            m_NetworkWriter.Write(rot);
            SendToAll(m_NetworkWriter);
        }
    }

    private void SendToOthers(NetworkWriter _writer, ulong ignore) {
        foreach (ulong guids in clients.Keys) {
            if (guids == ignore)
                continue;

            peer.SendData(guids, Peer.Reliability.Reliable, 0, _writer);
        }
    }

    private void SendToAll(NetworkWriter _writer) {
        foreach (ulong guids in clients.Keys) {
            peer.SendData(guids, Peer.Reliability.Reliable, 0, _writer);
        }
    }

    private void OnReceivedClientNetInfo(ulong guid) {
        Debug.Log("Received New Client Data");
        Connection connection = FindConnection(guid);

        if (connection != null) {
            if (connection.Info == null) {
                connection.Info = new ClientNetInfo();
                connection.Info.net_id = guid;
                connection.Info.name = m_NetworkReader.ReadString();
                connection.Info.local_id = m_NetworkReader.ReadPackedUInt64();
                connection.Info.client_hwid = m_NetworkReader.ReadString();
                connection.Info.client_version = m_NetworkReader.ReadString();
                ++clientID;

                if (m_NetworkWriter.StartWriting()) {
                    m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_WELCOME);
                    m_NetworkWriter.Write(clientID);
                    m_NetworkWriter.Write(clients.Count);

                    foreach (PlayerObject player in clients.Values) {
                        Debug.Log("Sending Player: " + player.id);
                        m_NetworkWriter.Write(player.id);
                        m_NetworkWriter.Write(player.name);
                        m_NetworkWriter.Write(player.pos);
                        m_NetworkWriter.Write(player.rot);
                        //m_NetworkWriter.Write(shipObj.id);
                        //m_NetworkWriter.Write(shipObj.m_x);
                        //m_NetworkWriter.Write(shipObj.m_y);
                        //m_NetworkWriter.Write(shipObj.shipNum);
                        //m_NetworkWriter.Write(shipObj.name);
                    }
                    peer.SendData(guid, Peer.Reliability.Reliable, 0, m_NetworkWriter);
                    //m_NetworkWriter.Send//sending
                    //m_NetworkWriter.Reset();

                    PlayerObject newObj = new PlayerObject(clientID);
                    //newObj.shipNum = m_NetworkReader.ReadInt32();
                    clients.Add(guid, newObj);
                }

                //if (m_NetworkWriter.StartWritting()) {
                //    m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_AISHIPS);
                //    m_NetworkWriter.Write(AIShips.Count);

                //    foreach (ShipObject shipObj in AIShips) {
                //        m_NetworkWriter.Write(shipObj.id);
                //        m_NetworkWriter.Write(shipObj.m_x);
                //        m_NetworkWriter.Write(shipObj.m_y);

                //        Debug.Log(shipObj.m_x);
                //    }
                //    peer.SendData(guid, Peer.Reliability.Reliable, 0, m_NetworkWriter);
                //}


                //peer.SendPacket(connection, Packets_ID.NET_LOGIN, Reliability.Reliable, m_NetworkWriter);
            }
            else {
                peer.SendPacket(connection, Packets_ID.CL_FAKE, Reliability.Reliable, m_NetworkWriter);
                peer.Kick(connection, 1);
            }
        }
    }
    #endregion
}
