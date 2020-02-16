using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Peer;
//using UnityEngine.UI;

//public struct ShipObject
//{
//    public uint id;
//    public float m_x;
//    public float m_y;
//    public float velocityX;
//    public float velocityY;
//    public float rotationVelocity;
//    public int shipNum;
//    public string name;
//    public float rotation;
//    public bool isShooting;
//    public float shootRate;

//    public ShipObject(uint _id)
//    {
//        this.shipNum = 1;
//        m_x = velocityX = velocityY = 0.0f;
//        m_y = 0.0f;
//        id = _id;
//        name = "";
//        rotation = rotationVelocity = 0.0f;
//        isShooting = false;
//        shootRate = 0.0f;
//    }
//}

//public class Server_Demo : MonoBehaviour
//{
//    public Peer peer { get; private set; }
//    private NetworkReader m_NetworkReader;
//    private NetworkWriter m_NetworkWriter;
//    public static Server_Demo Instance;
//    public Text Info;
//    public Rigidbody2D playerBeam;
//    public Rigidbody2D enemyBeam;
//    public GameObject powerUp;
//    private int numPowerUp = 0;
//    private float powerUpSpawn = 0f;

//    private Dictionary<ulong, ShipObject> clients = new Dictionary<ulong, ShipObject>();
//    //private List<Rigidbody2D> laserBeams = new List<Rigidbody2D>();
//    private List<ShipObject> AIShips = new List<ShipObject>();
//    private float AISpeed = 0.5f;
//    private uint shipID;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    public void Init(int _port)
//    {
//        StartServer("127.0.0.1", _port, 2);

//        for (uint i = 0; i < 5; ++i) {
//            ShipObject tempShip = new ShipObject(i);
//            tempShip.m_x = i;
//            tempShip.m_y = 2;

//            AIShips.Add(tempShip);
//        }

//    }

//    public void StopServer()
//    {
//        if (peer != null)
//        {
//            peer.Close();
//            Debug.LogError("[Server] Shutting down...");
//        }
//    }

//    private void OnDestroy()
//    {
//        StopServer();
//    }

//    public int MaxConnections { get; private set; } = -1;

//    public bool StartServer(string ip, int port, int maxConnections)
//    {
//        if (peer == null)
//        {
//            peer = new Peer();
//            peer = CreateServer(ip, port, maxConnections);

//            if (peer != null)
//            {
//                MaxConnections = maxConnections;
//                Debug.Log("[Server] Server initialized on port " + port);

//                Debug.Log("-------------------------------------------------");
//                Debug.Log("|     Max connections: " + maxConnections);
//                Debug.Log("|     Max FPS: " + (Application.targetFrameRate != -1 ? Application.targetFrameRate : 1000) + "(" + Time.deltaTime.ToString("f3") + " ms)");
//                Debug.Log("|     Tickrate: " + (1 / Time.fixedDeltaTime) + "(" + Time.fixedDeltaTime.ToString("f3") + " ms)");
//                Debug.Log("-------------------------------------------------");

//                m_NetworkReader = new NetworkReader(peer);
//                m_NetworkWriter = new NetworkWriter(peer);

//                return true;
//            }
//            else
//            {
//                Debug.LogError("[Server] Starting failed...");
//                return false;
//            }
//        }
//        else
//        {
//            return true;
//        }
//    }

//    private void FixedUpdate()
//    {
//        if (peer != null)
//        {
//            while (peer.Receive())
//            {
//                m_NetworkReader.StartReading();
//                byte b = m_NetworkReader.ReadByte();

//                OnReceivedPacket(b);
//            }
//        }

//        if (numPowerUp <= 3)
//        {
//            powerUpSpawn += Time.deltaTime;
//            if (powerUpSpawn >= 10f)
//            {
//                GameObject PU = Instantiate(powerUp, new Vector3(Random.Range(-3, 3), Random.Range(-2, 2)), new Quaternion(0, 0, 1, 0));
//                SendPowerUpData(PU);
//                numPowerUp++;
//                powerUpSpawn = 0f;
//            }
//        }

//        //for (int i = 0; i < laserBeams.Count; ++i) {
//        //    foreach (ShipObject shipObj in clients.Values) {
//        //        Rigidbody2D beam = laserBeams[i];
//        //        Vector2 beamPosition = beam.position;
//        //    }
//        //}

//        for(int i =0; i < AIShips.Count; ++i) {
//            foreach (ShipObject shipObj in clients.Values) {
//                ShipObject aiShip = AIShips[i];
//                Vector2 AIposition = new Vector2(aiShip.m_x, aiShip.m_y);
//                Vector2 shipPosition = new Vector2(shipObj.m_x, shipObj.m_y);
//                if (Vector2.Distance(AIposition, shipPosition) > 1f && Vector2.Distance(AIposition, shipPosition) < 3f) {
//                    float step = 1f * Time.deltaTime;
//                    Vector2 newPosition = Vector2.MoveTowards(AIposition, shipPosition, step);
//                    aiShip.m_x = newPosition.x;
//                    aiShip.m_y = newPosition.y;
//                    Vector2 direction = (shipPosition - AIposition).normalized;
//                    Quaternion _lookRotation = Quaternion.LookRotation(direction);
//                    aiShip.rotation = _lookRotation.eulerAngles.z;
//                    aiShip.isShooting = true;
//                    AIShips[i] = aiShip;
//                    SendAIPosition();
//                }

//                if (aiShip.isShooting) {
//                    aiShip.shootRate += Time.deltaTime;
//                    if (aiShip.shootRate >= 0.2f) {
//                        Quaternion qRotation = Quaternion.Euler(0, 0, aiShip.rotation);
//                        Rigidbody2D p = Instantiate(enemyBeam, new Vector2(aiShip.m_x, aiShip.m_y), qRotation);
//                        p.velocity = p.gameObject.transform.up * 5;
//                        SendLaserBeamData(p);
//                        aiShip.shootRate = 0f;
//                    }
//                }
//            }
//        }
//    }


//    private void OnReceivedPacket(byte packet_id)
//    {
//        bool IsInternalNetworkPackets = packet_id <= 134;

//        if (IsInternalNetworkPackets)
//        {
//            if (packet_id == (byte)RakNet_Packets_ID.NEW_INCOMING_CONNECTION)
//            {
//                OnConnected();
//            }

//            if (packet_id == (byte)RakNet_Packets_ID.CONNECTION_LOST || packet_id == (byte)RakNet_Packets_ID.DISCONNECTION_NOTIFICATION)
//            {
//                Connection conn = FindConnection(peer.incomingGUID);

//                if (conn != null)
//                {
//                    OnDisconnected(FindConnection(peer.incomingGUID));

//                }
//            }
//        }
//        else
//        {
//            switch(packet_id)
//            {
//                case (byte)Packets_ID.CL_INFO:
//                    OnReceivedClientNetInfo(peer.incomingGUID);
//                    break;
//                case (byte)Packets_ID.ID_INITIALSTATS:
//                    OnReceivedClientInitialStats(peer.incomingGUID);
//                    break;
//                case (byte)Packets_ID.ID_MOVEMENT:
//                    OnReceivedClientMovementData(peer.incomingGUID);
//                    break;
//                case (byte)Packets_ID.NET_CHAT:
//                    OnReceivedChatData(peer.incomingGUID);
//                    break;
//            }
//        }
//    }



//    #region Connections
//    public List<Connection> connections = new List<Connection>();
//    private Dictionary<ulong, Connection> connectionByGUID = new Dictionary<ulong, Connection>();

//    public List<ulong> guids = new List<ulong>();

//    public Connection FindConnection(ulong guid)
//    {
//        if (connectionByGUID.TryGetValue(guid, out Connection value))
//        {
//            return value;
//        }
//        return null;
//    }

//    private void AddConnection(Connection connection)
//    {
//        connections.Add(connection);
//        connectionByGUID.Add(connection.guid, connection);
//        guids.Add(connection.guid);
//    }

//    private void RemoveConnection(Connection connection)
//    {
//        clients.Remove(connection.guid);
//        connectionByGUID.Remove(connection.guid);
//        connections.Remove(connection);
//        guids.Remove(connection.guid);
//    }

//    public static Connection[] Connections
//    {
//        get
//        {
//            return Instance.connections.ToArray();
//        }
//    }

//    public static Connection GetByID(int id)
//    {
//        if (Connections.Length > 0)
//        {
//            return Connections[id];
//        }

//        return null;
//    }

//    public static Connection GetByIP(string ip)
//    {
//        foreach (Connection c in Connections)
//        {
//            if (c.ipaddress == ip)
//            {
//                return c;
//            }
//        }

//        return null;
//    }

//    public static Connection GetByName(string name)
//    {
//        foreach (Connection c in Connections)
//        {
//            if (c.Info.name == name)
//            {
//                return c;
//            }
//        }

//        return null;
//    }

//    public static Connection GetByHWID(string hwid)
//    {
//        foreach (Connection c in Connections)
//        {
//            if (c.Info.client_hwid == hwid)
//            {
//                return c;
//            }
//        }

//        return null;
//    }

//    #endregion

//    #region Events
//    private void OnConnected()
//    {
//        Connection connection = new Connection(peer, peer.incomingGUID, connections.Count);

//        //добавляем в список соединений
//        AddConnection(connection);

//        Debug.Log("[Server] Connection established " + connection.ipaddress);
//        //peer.SendData(guid, Peer.Reliability.Reliable, 0, m_NetworkWriter);
      
//        peer.SendPacket(connection, Packets_ID.CL_INFO, m_NetworkWriter);
//    }

//    private void OnDisconnected(Connection connection)
//    {
//        if (connection != null)
//        {
//            try
//            {
//                Debug.LogError("[Server] " + connection.Info.name + " disconnected [IP: " + connection.ipaddress + "]");

//                RemoveConnection(connection);
//            }
//            catch
//            {
//                Debug.LogError("[Server] Unassigned connection destroyed!");
//            }
//        }
//    }
    
//    private void SendAIPosition()
//    {
//        if (m_NetworkWriter.StartWritting()) {
//            m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_AISHIPSMOVEMENT);
//            m_NetworkWriter.Write(AIShips.Count);

//            foreach (ShipObject tempObj in AIShips) {
//                m_NetworkWriter.Write(tempObj.id);
//                m_NetworkWriter.Write(tempObj.m_x);
//                m_NetworkWriter.Write(tempObj.m_y);
//                m_NetworkWriter.Write(tempObj.rotation);
//            }
//            SendToAll(m_NetworkWriter);
//        }
//    }

//    private void SendPowerUpData(GameObject pu)
//    {
//        if (m_NetworkWriter.StartWritting())
//        {
//            m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_POWERUP);
//            m_NetworkWriter.Write(pu.transform.position);
//            SendToAll(m_NetworkWriter);
//        }
//    }
//    private void SendLaserBeamData(Rigidbody2D beam) {
//        if (m_NetworkWriter.StartWritting()) {
//            m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_LASERBEAM);
//            //m_NetworkWriter.Write(laserBeams.Count);

//            m_NetworkWriter.Write(beam.position);
//            m_NetworkWriter.Write(beam.velocity);
//            m_NetworkWriter.Write(beam.transform.rotation);
//            //SendToAll(0, m_NetworkWriter, false);
//            SendToAll(m_NetworkWriter);
//        }
//    }

//    private void OnReceivedChatData(ulong guid) {

//        string chatString = m_NetworkReader.ReadString();

//        if (m_NetworkWriter.StartWritting()) {
//            m_NetworkWriter.WritePacketID((byte)Packets_ID.NET_CHAT);
//            m_NetworkWriter.Write(chatString);
//            SendToAll(m_NetworkWriter);
//        }
//    }

//    private void OnReceivedClientMovementData(ulong guid)
//    {
//        ShipObject tempObj = clients[guid];
//        tempObj.m_x = m_NetworkReader.ReadFloat();
//        tempObj.m_y = m_NetworkReader.ReadFloat();
//        tempObj.rotation = m_NetworkReader.ReadFloat();
//        tempObj.velocityX = m_NetworkReader.ReadFloat();
//        tempObj.velocityY = m_NetworkReader.ReadFloat();
//        tempObj.rotationVelocity = m_NetworkReader.ReadFloat();
//        tempObj.isShooting = m_NetworkReader.ReadBoolean();

//        if (tempObj.isShooting) {
//            Quaternion qRotation = Quaternion.Euler(0, 0, tempObj.rotation);
//            Rigidbody2D p = Instantiate(playerBeam, new Vector2(tempObj.m_x, tempObj.m_y), qRotation);
//            p.velocity = p.gameObject.transform.up * 5;
//            SendLaserBeamData(p);
//        }


//        clients[guid] = tempObj;

//        if (m_NetworkWriter.StartWritting())
//        {
//            m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_MOVEMENT);
//            m_NetworkWriter.Write(tempObj.id);
//            m_NetworkWriter.Write(tempObj.m_x);
//            m_NetworkWriter.Write(tempObj.m_y);
//            m_NetworkWriter.Write(tempObj.rotation);
//            m_NetworkWriter.Write(tempObj.velocityX);
//            m_NetworkWriter.Write(tempObj.velocityY);
//            m_NetworkWriter.Write(tempObj.rotationVelocity);
//            m_NetworkWriter.Write(tempObj.isShooting);

//            //SendToAll(guid, m_NetworkWriter, true);
//            SendToOthers(m_NetworkWriter, guid);
//        }
//    }
//    private void OnReceivedClientInitialStats(ulong guid)
//    {
//        ShipObject tempObj = clients[guid];
//        tempObj.name = m_NetworkReader.ReadString();
//        tempObj.m_x = m_NetworkReader.ReadFloat();
//        tempObj.m_y = m_NetworkReader.ReadFloat();

//        clients[guid] = tempObj;

//        if (m_NetworkWriter.StartWritting())
//        {
//            m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_NEWSHIP);
//            m_NetworkWriter.Write(tempObj.id);
//            m_NetworkWriter.Write(tempObj.name);
//            m_NetworkWriter.Write(tempObj.m_x);
//            m_NetworkWriter.Write(tempObj.m_y);
//            m_NetworkWriter.Write(tempObj.shipNum);

//            //SendToAll(guid, m_NetworkWriter, true);
//            SendToOthers(m_NetworkWriter, guid);
//            //peer.SendBroadcast(Peer.Priority.Immediate, Peer.Reliability.Reliable, 0);
         
//        }
//    }

//    private void SendToOthers(NetworkWriter _writer, ulong ignore) {
//        foreach (ulong guids in clients.Keys) {
//            if (guids == ignore)
//                continue;

//            peer.SendData(guids, Peer.Reliability.Reliable, 0, _writer);
//        }
//    }

//    private void SendToAll(NetworkWriter _writer) {
//        foreach (ulong guids in clients.Keys) {
//            peer.SendData(guids, Peer.Reliability.Reliable, 0, _writer);
//        }
//    }
    
//    private void OnReceivedClientNetInfo(ulong guid)
//    {
//        Debug.Log("server received data");
//        Connection connection = FindConnection(guid);

//        if (connection != null)
//        {
//            if (connection.Info == null)
//            {
//                connection.Info = new ClientNetInfo();
//                connection.Info.net_id = guid;
//                connection.Info.name = m_NetworkReader.ReadString();
//                connection.Info.local_id = m_NetworkReader.ReadPackedUInt64();
//                connection.Info.client_hwid = m_NetworkReader.ReadString();
//                connection.Info.client_version = m_NetworkReader.ReadString();
//                ++shipID;

//                Debug.Log("Sent");

//                if (m_NetworkWriter.StartWritting())
//                {
//                    m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_WELCOME);
//                    m_NetworkWriter.Write(shipID);
//                    m_NetworkWriter.Write(clients.Count);
                    
//                   foreach (ShipObject shipObj in clients.Values)
//                    {
//                        m_NetworkWriter.Write(shipObj.id);
//                        m_NetworkWriter.Write(shipObj.m_x);
//                        m_NetworkWriter.Write(shipObj.m_y);
//                        m_NetworkWriter.Write(shipObj.shipNum);
//                        m_NetworkWriter.Write(shipObj.name);
//                    }
//                   peer.SendData(guid, Peer.Reliability.Reliable, 0, m_NetworkWriter);
//                    //m_NetworkWriter.Send//sending
//                    //m_NetworkWriter.Reset();

//                    ShipObject newObj = new ShipObject(shipID);
//                    newObj.shipNum = m_NetworkReader.ReadInt32();
//                    clients.Add(guid, newObj);

//                    Debug.Log("Added new guy : " + newObj.id);
//                }

//                if (m_NetworkWriter.StartWritting()) {
//                    m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_AISHIPS);
//                    m_NetworkWriter.Write(AIShips.Count);

//                    foreach (ShipObject shipObj in AIShips) {
//                        m_NetworkWriter.Write(shipObj.id);
//                        m_NetworkWriter.Write(shipObj.m_x);
//                        m_NetworkWriter.Write(shipObj.m_y);

//                        Debug.Log(shipObj.m_x);
//                    }
//                    peer.SendData(guid, Peer.Reliability.Reliable, 0, m_NetworkWriter);
//                }


//                //peer.SendPacket(connection, Packets_ID.NET_LOGIN, Reliability.Reliable, m_NetworkWriter);
//            }
//            else
//            {
//                peer.SendPacket(connection, Packets_ID.CL_FAKE, Reliability.Reliable, m_NetworkWriter);
//                peer.Kick(connection, 1);
//            }
//        }
//    }
//    #endregion

//    /*
//    public InputField Guid;

//    public void OnBanClicked()
//    {
//        Connection connection = FindConnection(ulong.Parse(Guid.text));

//        if (connection != null)
//        {
//            peer.SendPacket(connection, Packets_ID.CL_BANNED, Reliability.Reliable, m_NetworkWriter);
//            peer.Kick(connection, 1);
//        }
//    }
//    */
//}