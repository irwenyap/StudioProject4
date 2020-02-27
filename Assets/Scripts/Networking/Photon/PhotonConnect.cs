using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text nameText = null;

    public void Connect() {
        Debug.Log("[Connecting to Server]");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nameText.text;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        Debug.Log("[Connected to Server]");
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);

        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.LogError("[Disconnected from Server: " + cause.ToString() + "]");
    }
}
