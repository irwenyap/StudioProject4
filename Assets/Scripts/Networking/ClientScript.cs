using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientScript : MonoBehaviour {
    public ClientNetInfo m_ClientNetInfo = new ClientNetInfo();
    public enum ClientState { DISCONNECTED, CONNECTED }
    public ClientState m_State = ClientState.DISCONNECTED;
    public Text Info;
    public Peer peer { get; private set; }

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private WeaponManager weaponManager;
    //public GameObject enemyShipReference;

    private List<GameObject> playerList = new List<GameObject>();

    private NetworkReader m_NetworkReader;
    private NetworkWriter m_NetworkWriter;
    public static ClientScript Instance;

    private bool sendMsg = false;
    private ulong serveruid = 0;
    private float delta = 0.0f;
    private string userName;
    private int frameSplit;

    private void Awake() {
        Instance = this;
    }

    public void Init(string _ip, int _port, string _name) {
        userName = _name;
        Connect(_ip, _port);
    }

    public bool IsRunning {
        get {
            return peer != null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (frameSplit >= 60) {
            SendMovement();
        }

        ++frameSplit;
    }

    private unsafe void FixedUpdate() {
        m_State = peer != null ? ClientState.CONNECTED : ClientState.DISCONNECTED;

        if (peer != null) {
            while (peer.Receive()) {
                m_NetworkReader.StartReading();
                byte b = m_NetworkReader.ReadByte();

                OnReceivedPacket(b);
            }
        }
    }

    #region Connect/Disconnect
    public void Connect(string ip, int port, int retries, int retry_delay, int timeout) {
    CREATE_PEER:
        //tmp_Banned = tmp_Fake = false;
        if (peer == null) {
            peer = Peer.CreateConnection(ip, port, retries, retry_delay, timeout);

            if (peer != null) {
                Debug.Log("[Client] Preparing to receiving...");
                m_NetworkReader = new NetworkReader(peer);
                m_NetworkWriter = new NetworkWriter(peer);
            }
        }
        else {
            peer.Close();
            peer = null;

            goto CREATE_PEER;
        }
    }

    public void Connect(string ip, int port) {
        Connect(ip, port, 30, 500, 30);
    }


    public void Disconnect() {
        if (m_State == ClientState.CONNECTED) {
            OnDisconnected("");
            peer.Close();
            peer = null;
        }
    }
    #endregion

    private void OnReceivedPacket(byte packet_id) {
        bool IsInternalNetworkPackets = packet_id <= 134;

        if (IsInternalNetworkPackets) {
            if (packet_id == (byte)Peer.RakNet_Packets_ID.CONNECTION_REQUEST_ACCEPTED) {
                OnConnected(peer.incomingAddress);
            }

            if (packet_id == (byte)Peer.RakNet_Packets_ID.CONNECTION_ATTEMPT_FAILED) {
                OnDisconnected("Connection attempt failed");
            }

            if (packet_id == (byte)Peer.RakNet_Packets_ID.INCOMPATIBLE_PROTOCOL_VERSION) {
                OnDisconnected("Incompatible protocol version");
            }

            if (packet_id == (byte)Peer.RakNet_Packets_ID.CONNECTION_LOST) {
                OnDisconnected("Time out");
            }

            if (packet_id == (byte)Peer.RakNet_Packets_ID.NO_FREE_INCOMING_CONNECTIONS) {
                OnDisconnected("Server is full.");
            }

            //if (packet_id == (byte)Peer.RakNet_Packets_ID.DISCONNECTION_NOTIFICATION && !tmp_Banned && !tmp_Fake) {
            //    OnDisconnected("You are kicked!");
            //}
        }
        else {
            switch (packet_id) {
                case (byte)Packets_ID.CL_INFO:
                    if (m_NetworkWriter.StartWriting()) {
                        GameObject player = Instantiate(playerPrefab);
                        player.GetComponent<PlayerController>().Initialise(true);
                        playerList.Add(player);
                        //int shipNum = player.GetComponentInChildren<PlayerController>().Init(true);
                        //player.GetComponent<PlayerController>().SetShip(true);

                        m_NetworkWriter.WritePacketID((byte)Packets_ID.CL_INFO);
                        m_NetworkWriter.Write(m_ClientNetInfo.name);
                        m_NetworkWriter.WritePackedUInt64(m_ClientNetInfo.local_id);
                        m_NetworkWriter.Write(m_ClientNetInfo.client_hwid);
                        m_NetworkWriter.Write(m_ClientNetInfo.client_version);

                        //m_NetworkWriter.Write(shipNum);
                        serveruid = peer.incomingGUID;
                        m_NetworkWriter.Send(peer.incomingGUID, Peer.Priority.Immediate, Peer.Reliability.Reliable, 0);//sending
                    }
                    break;
                case (byte)Packets_ID.NET_REGISTER: {
                        bool success = m_NetworkReader.ReadBoolean();

                        if (success) {
                            GameObject obj = GameObject.FindGameObjectWithTag("UIMaster");
                            obj.GetComponent<UIController>().SetToLogin();
                        }
                        else
                            Debug.Log("Username Already Exists");
                    }
                    break;
                case (byte)Packets_ID.CL_ACCEPTED:
                    m_ClientNetInfo.net_id = m_NetworkReader.ReadPackedUInt64();
                    Debug.Log("[Client] Accepted connection by server... [ID: " + m_ClientNetInfo.net_id + "]");
                    break;
                case (byte)Packets_ID.ID_WELCOME: {
                        Debug.Log("welcome!!");
                        GameObject player = playerList[0];
                        PlayerController playerController = player.GetComponent<PlayerController>();
                        uint id = m_NetworkReader.ReadUInt32();
                        playerController.id = id;
                        int clientCount = m_NetworkReader.ReadInt32();

                        for (int i = 0; i < clientCount; ++i) {
                            GameObject otherPlayer = Instantiate(playerPrefab);
                            PlayerController otherController = otherPlayer.GetComponent<PlayerController>();
                            otherController.id = m_NetworkReader.ReadUInt32();
                            otherController.playerName = m_NetworkReader.ReadString();
                            otherPlayer.transform.position = m_NetworkReader.ReadVector2();
                            otherPlayer.transform.rotation = m_NetworkReader.ReadQuaternion();
                            playerList.Add(otherPlayer);
                            //otherManager.position = new Vector3(m_NetworkReader.ReadFloat(), m_NetworkReader.ReadFloat(), 0);
                            //otherManager.SetImg(m_NetworkReader.ReadInt32());
                            //otherManager.pName = m_NetworkReader.ReadString();
                            //shipList.Add(otherManager);
                        }
                        SendInitialStats();
                    }
                    break;
                case (byte)Packets_ID.ID_MOVEMENT:
                    uint clientID = m_NetworkReader.ReadUInt32();
                    foreach (GameObject player in playerList) {
                        PlayerController playerController = player.GetComponent<PlayerController>();
                        if (playerController.id == clientID) {
                            player.transform.position = m_NetworkReader.ReadVector2();
                            player.transform.rotation = m_NetworkReader.ReadQuaternion();
                        }
                    }
                    //foreach (ShipManager ship in shipList) {
                    //    if (ship.pid == shipID) {
                    //        //  ship.position = new Vector3(m_NetworkReader.ReadFloat(), m_NetworkReader.ReadFloat(), 0);
                    //        //  ship.pRotation = m_NetworkReader.ReadFloat();

                    //        // Step 8 : Instead of using ship.position, use server_pos and serverRotation
                    //        // set server position, server rotation\
                    //        ship.server_pos = new Vector3(m_NetworkReader.ReadFloat(), m_NetworkReader.ReadFloat(), 0);
                    //        ship.serverRotation = m_NetworkReader.ReadFloat();

                    //        // Lab 7 Task 1 : Read Extrapolation Data velocity x, velocity y & angular velocity
                    //        // set velocity and rotation velocity of ship (look at ship Manager)

                    //        ship.velocity = new Vector3(m_NetworkReader.ReadFloat(), m_NetworkReader.ReadFloat(), 0);
                    //        ship.rotationVelocity = m_NetworkReader.ReadFloat();

                    //    }
                    //}
                    break;
                case (byte)Packets_ID.ID_NEWCLIENT: {
                        GameObject newPlayer = Instantiate(playerPrefab);
                        PlayerController otherController = newPlayer.GetComponent<PlayerController>();
                        otherController.id = m_NetworkReader.ReadUInt32();
                        otherController.playerName = m_NetworkReader.ReadString();
                        otherController.Initialise(false);
                        newPlayer.transform.position = m_NetworkReader.ReadVector2();
                        newPlayer.transform.rotation = m_NetworkReader.ReadQuaternion();
                        playerList.Add(newPlayer);

                        //GameObject otherShip = Instantiate(shipReference);
                        //ShipManager otherManager = otherShip.GetComponent<ShipManager>();
                        //otherManager.pid = m_NetworkReader.ReadUInt32();
                        //otherShip.GetComponentInChildren<ShipMovement>().Init(false);
                        //otherManager.pName = m_NetworkReader.ReadString();
                        //otherManager.position = new Vector3(m_NetworkReader.ReadFloat(), m_NetworkReader.ReadFloat(), 0);
                        //int simg = m_NetworkReader.ReadInt32();
                        //otherManager.SetImg(simg);
                        ////m_NetworkReader.
                        //Debug.Log("Received Img: " + simg);

                        //shipList.Add(otherManager);
                    }
                    break;
                case (byte)Packets_ID.ID_WEAPONDATA: {
                        Vector2 rigidbodyPos = m_NetworkReader.ReadVector2();
                        Quaternion rigidbodyRot = m_NetworkReader.ReadQuaternion();


                        Rigidbody2D myRigidbody = Instantiate(weaponManager.bullet, rigidbodyPos, rigidbodyRot);
                        myRigidbody.velocity = myRigidbody.gameObject.transform.up * 5;
                    }
                    break;
                case (byte)Packets_ID.ID_AISHIPS: {
                        //int shipCount = m_NetworkReader.ReadInt32();

                        //for (int i = 0; i < shipCount; ++i) {
                        //    GameObject otherShip = Instantiate(enemyShipReference);
                        //    otherShip.GetComponentInChildren<ShipMovement>().Init(false);
                        //    ShipManager otherManager = otherShip.GetComponent<ShipManager>();
                        //    otherManager.pid = m_NetworkReader.ReadUInt32();
                        //    otherManager.position = new Vector3(m_NetworkReader.ReadFloat(), m_NetworkReader.ReadFloat(), 0);
                        //    Debug.Log("client " + otherManager.position);
                        //    otherManager.SetImg(2);
                        //    otherManager.pName = "Enemy " + i;
                        //    otherManager.server_pos = otherManager.position;
                        //    enemyDict[otherManager.pid] = otherManager;
                        //}
                    }
                    break;
                case (byte)Packets_ID.ID_AISHIPSMOVEMENT: {
                        //int shipCount = m_NetworkReader.ReadInt32();

                        //for (int i = 0; i < shipCount; ++i) {
                        //    uint shipid = m_NetworkReader.ReadUInt32();

                        //    ShipManager shipObj = enemyDict[shipid];
                        //    shipObj.server_pos = new Vector3(m_NetworkReader.ReadFloat(), m_NetworkReader.ReadFloat(), 0);
                        //    shipObj.velocity = Vector3.forward;
                        //    shipObj.pRotation = m_NetworkReader.ReadFloat();
                        //}
                    }
                    break;
            }
        }
    }

    private void SendInitialStats() {
        if (m_NetworkWriter.StartWriting()) {
            GameObject client = playerList[0];
            PlayerController clientController = client.GetComponent<PlayerController>();
            clientController.playerName = userName;
            m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_INITIALSTATS);
            m_NetworkWriter.Write(clientController.playerName);
            m_NetworkWriter.Write(new Vector2(client.transform.position.x, client.transform.position.y));
            m_NetworkWriter.Write(client.transform.rotation);
            //ShipManager me = shipList[0];
            //me.pName = userName;
            //m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_INITIALSTATS);
            //m_NetworkWriter.Write(me.pName);
            //m_NetworkWriter.Write(me.position.x);
            //m_NetworkWriter.Write(me.position.y);

            m_NetworkWriter.Send(serveruid, Peer.Priority.Immediate, Peer.Reliability.Reliable, 0);
        }
    }

    private void SendMovement() {
        if (m_NetworkWriter.StartWriting()) {
            GameObject me = playerList[0];
            PlayerController meController = me.GetComponent<PlayerController>();
            m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_MOVEMENT);
            m_NetworkWriter.Write(new Vector2(me.transform.position.x, me.transform.position.y));
            m_NetworkWriter.Write(me.transform.rotation);
            m_NetworkWriter.Write(meController.useWeapon);
            //// step 9 : Instead of sending x,y,w ..... , send the server version instead (x,y,w,velocity, angular velocity)

            //m_NetworkWriter.Write(me.position.x);
            //m_NetworkWriter.Write(me.position.y);
            //m_NetworkWriter.Write(me.pRotation);
            //// Lab 7 Task 1 : Add Extrapolation Data velocity x, velocity y & angular velocity
            //// Send ShipManager velocity x, y and rotaion velocity
            //m_NetworkWriter.Write(me.velocity.x);
            //m_NetworkWriter.Write(me.velocity.y);
            //m_NetworkWriter.Write(me.rotationVelocity);

            //// Shooting
            //if (weaponType == 0)
            //    m_NetworkWriter.Write(me.bisShooting);
            //else
            //    m_NetworkWriter.Write(false);

            m_NetworkWriter.Send(serveruid, Peer.Priority.Immediate, Peer.Reliability.Reliable, 0);
        }
    }

    private void OnConnected(string address) {
        Debug.Log("[Client] Connected to " + address);

        m_ClientNetInfo.name = "Player_" + Environment.MachineName;
        m_ClientNetInfo.local_id = peer.incomingGUID;
        m_ClientNetInfo.client_hwid = SystemInfo.deviceUniqueIdentifier;
        m_ClientNetInfo.client_version = Application.version;
    }

    private void OnDisconnected(string reason) {
        Debug.LogError("[Client] Disconnected" + (reason.Length > 0 ? " with reason: " + reason : "..."));
    }
}
