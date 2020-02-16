using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client_Demo : MonoBehaviour {
    public ClientNetInfo m_ClientNetInfo = new ClientNetInfo();
    public enum ClientState { DISCONNECTED, CONNECTED }
    public ClientState m_State = ClientState.DISCONNECTED;
    public Text Info;
    public Rigidbody2D playerBeam;
    public Rigidbody2D enemyBeam;
    public GameObject powerUp;
    public Peer peer { get; private set; }

    [SerializeField]
    private GameObject shipReference;
    public GameObject enemyShipReference;

    //private List<ShipManager> shipList = new List<ShipManager>();
    //private List<ShipManager> enemyShipList = new List<ShipManager>();
    //private Dictionary<uint, ShipManager> enemyDict = new Dictionary<uint, ShipManager>();

    private NetworkReader m_NetworkReader;
    private NetworkWriter m_NetworkWriter;
    public static Client_Demo Instance;

    private bool sendMsg = false;
    private ulong serveruid = 0;
    private float delta = 0.0f;
    private string userName;
    public int weaponType = 1;
    public float weaponTimer = 0f;

    // Chat
    GameObject chatButton;
    List<string> chatDisplay = new List<string>();

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


    protected void Update() {
        delta += Time.deltaTime;

        if (delta > 0.2f) {
            SendMovement();
            delta = 0;
        }

        //if (weaponType == 1 && weaponTimer <= 5f) {
        //    //Debug.Log("LASER BEAM ++++++++++++++++++++++++++++++");
        //    weaponTimer += Time.deltaTime;
        //    ShipManager me = shipList[0];
        //    GameObject childShip = shipList[0].transform.Find("ship1").gameObject;
        //    GameObject childLaser = childShip.transform.Find("laserbeam").gameObject;
        //    if (Input.GetKey(KeyCode.Space)) {
        //        childLaser.SetActive(true);
        //    }
        //    else {
        //        childLaser.SetActive(false);
        //    }
        //}
    }

    public void AddText(string newInput) {
        if (chatDisplay.Count == 9) {
            chatDisplay.RemoveAt(0);
        }
        chatDisplay.Add(newInput);
        UpdateText();
    }

    public void UpdateText() {
        //string wholeText = "";
        //foreach (string s in chatDisplay) {
        //    string strDate = s as string;
        //    wholeText = (wholeText + "\n" + strDate);
        //}
        //chatButton.GetComponent<SendMessageButton>().displayChat.GetComponent<Text>().text = wholeText;
    }

    public void SendClientMessage(string chatText) {
        //if (m_NetworkWriter.StartWritting()) {
        //    string sendText = shipList[0].pName + ": " + chatText;
        //    m_NetworkWriter.WritePacketID((byte)Packets_ID.NET_CHAT);
        //    m_NetworkWriter.Write(sendText);
        //    m_NetworkWriter.Send(serveruid, Peer.Priority.Immediate, Peer.Reliability.Reliable, 0);
        //    //displayChat.GetComponent<Text>().text = sendText;
        //    Debug.Log("Message Sent");
        //}
    }

    public void SetChatButton(GameObject _chatButton) {
        chatButton = _chatButton;
    }

    private void SendMovement() {
        //if (m_NetworkWriter.StartWritting()) {
        //    ShipManager me = shipList[0];
        //    m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_MOVEMENT);

        //    // step 9 : Instead of sending x,y,w ..... , send the server version instead (x,y,w,velocity, angular velocity)

        //    m_NetworkWriter.Write(me.position.x);
        //    m_NetworkWriter.Write(me.position.y);
        //    m_NetworkWriter.Write(me.pRotation);
        //    // Lab 7 Task 1 : Add Extrapolation Data velocity x, velocity y & angular velocity
        //    // Send ShipManager velocity x, y and rotaion velocity
        //    m_NetworkWriter.Write(me.velocity.x);
        //    m_NetworkWriter.Write(me.velocity.y);
        //    m_NetworkWriter.Write(me.rotationVelocity);

        //    // Shooting
        //    if (weaponType == 0)
        //        m_NetworkWriter.Write(me.bisShooting);
        //    else
        //        m_NetworkWriter.Write(false);

        //    m_NetworkWriter.Send(serveruid, Peer.Priority.Immediate, Peer.Reliability.Reliable, 0);
        //}
    }

    #region Connect/Disconnect
    public void Connect(string ip, int port, int retries, int retry_delay, int timeout) {
    CREATE_PEER:
        tmp_Banned = tmp_Fake = false;
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


    private bool tmp_Banned = false, tmp_Fake = false;


    /// <summary>
    /// Parsing packet
    /// </summary>
    /// <param name="packet_id">PACKET ID  - SEE Packets_ID.cs</param>

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

            if (packet_id == (byte)Peer.RakNet_Packets_ID.DISCONNECTION_NOTIFICATION && !tmp_Banned && !tmp_Fake) {
                OnDisconnected("You are kicked!");
            }
        }
        else {
            switch (packet_id) {
                case (byte)Packets_ID.CL_INFO:
                //    if (m_NetworkWriter.StartWritting()) {
                //        GameObject shipObj = Instantiate(shipReference);
                //        int shipNum = shipObj.GetComponentInChildren<ShipMovement>().Init(true);
                //        shipObj.GetComponent<ShipManager>().SetShip(true);
                //        shipList.Add(shipObj.GetComponent<ShipManager>());

                //        m_NetworkWriter.WritePacketID((byte)Packets_ID.CL_INFO);
                //        m_NetworkWriter.Write(m_ClientNetInfo.name);
                //        m_NetworkWriter.WritePackedUInt64(m_ClientNetInfo.local_id);
                //        m_NetworkWriter.Write(m_ClientNetInfo.client_hwid);
                //        m_NetworkWriter.Write(m_ClientNetInfo.client_version);
                //        m_NetworkWriter.Write(shipNum);
                //        serveruid = peer.incomingGUID;
                //        m_NetworkWriter.Send(peer.incomingGUID, Peer.Priority.Immediate, Peer.Reliability.Reliable, 0);//sending
                //    }
                //    break;
                //case (byte)Packets_ID.NET_REGISTER: {
                //        bool success = m_NetworkReader.ReadBoolean();

                //        if (success) {
                //            GameObject obj = GameObject.FindGameObjectWithTag("UIMaster");
                //            obj.GetComponent<UIController>().SetToLogin();
                //        }
                //        else
                //            Debug.Log("Username Already Exists");
                //    }
                    break;
                case (byte)Packets_ID.CL_ACCEPTED:
                    //m_ClientNetInfo.net_id = m_NetworkReader.ReadPackedUInt64();
                    //Debug.Log("[Client] Accepted connection by server... [ID: " + m_ClientNetInfo.net_id + "]");
                    break;
                case (byte)Packets_ID.ID_WELCOME: {
                    //    Debug.Log("welcome!!");
                    //    uint id = m_NetworkReader.ReadUInt32();
                    //    ShipManager mgr = shipList[0];
                    //    mgr.pid = id;
                    //    int shipCount = m_NetworkReader.ReadInt32();

                    //    for (int i = 0; i < shipCount; ++i) {
                    //        GameObject otherShip = Instantiate(shipReference);
                    //        ShipManager otherManager = otherShip.GetComponent<ShipManager>();
                    //        otherManager.pid = m_NetworkReader.ReadUInt32();
                    //        otherManager.position = new Vector3(m_NetworkReader.ReadFloat(), m_NetworkReader.ReadFloat(), 0);
                    //        otherManager.SetImg(m_NetworkReader.ReadInt32());
                    //        otherManager.pName = m_NetworkReader.ReadString();
                    //        shipList.Add(otherManager);
                    //    }
                    //    SendInitialStats();
                    }
                    break;
                case (byte)Packets_ID.ID_MOVEMENT:
                    uint shipID = m_NetworkReader.ReadUInt32();

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
                case (byte)Packets_ID.ID_NEWSHIP: {
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
                case (byte)Packets_ID.ID_LASERBEAM: {
                        Vector2 beamPos = m_NetworkReader.ReadVector2();
                        Vector2 beamVel = m_NetworkReader.ReadVector2();
                        Quaternion beamRot = m_NetworkReader.ReadQuaternion();


                        Rigidbody2D beam = Instantiate(playerBeam, beamPos, beamRot);
                        beam.velocity = beamVel;
                    }
                    break;
                case (byte)Packets_ID.ID_POWERUP: {
                        Vector3 puPos = m_NetworkReader.ReadVector3();

                        _ = Instantiate(powerUp, puPos, new Quaternion(0, 0, 1, 0));
                    }
                    break;
                case (byte)Packets_ID.NET_CHAT: {
                        string rcvText = m_NetworkReader.ReadString();
                        AddText(rcvText);
                    }
                    break;
            }
        }
    }

    //private void SendInitialStats() {
    //    if (m_NetworkWriter.StartWritting()) {
    //        ShipManager me = shipList[0];
    //        me.pName = userName;
    //        m_NetworkWriter.WritePacketID((byte)Packets_ID.ID_INITIALSTATS);
    //        m_NetworkWriter.Write(me.pName);
    //        m_NetworkWriter.Write(me.position.x);
    //        m_NetworkWriter.Write(me.position.y);

    //        m_NetworkWriter.Send(serveruid, Peer.Priority.Immediate, Peer.Reliability.Reliable, 0);
    //    }
    //}

    private void OnConnected(string address) {
        Debug.Log("[Client] Connected to " + address);

        //формируем/готовим информацию клиента
        m_ClientNetInfo.name = "Player_" + Environment.MachineName;
        m_ClientNetInfo.local_id = peer.incomingGUID;
        m_ClientNetInfo.client_hwid = SystemInfo.deviceUniqueIdentifier;
        m_ClientNetInfo.client_version = Application.version;
    }

    private void OnDisconnected(string reason) {
        Debug.LogError("[Client] Disconnected" + (reason.Length > 0 ? " with reason: " + reason : "..."));
    }
}
