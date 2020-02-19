using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostLobby : MonoBehaviourPunCallbacks {
    [SerializeField]
    private Text _roomName;
    private Button myButton;

    private void Start() {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(CreateLobby);
    }

    private void CreateLobby() {
        if (!PhotonNetwork.IsConnected)
            return;
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom() {
        Debug.Log("[Room Creation Successful]");
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.LogError("--Room Creation Failed--");
        Debug.LogError(message);
    }
}
