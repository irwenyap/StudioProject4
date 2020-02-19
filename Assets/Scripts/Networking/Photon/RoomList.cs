﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomList : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    private Button myButton;

    private void Start() {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(JoinLobby);
    }

    public RoomInfo _roomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo) {
        _roomInfo = roomInfo;
        _text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }

    public void JoinLobby() {
        Debug.Log("Joining Lobby");
        PhotonNetwork.JoinRoom(_roomInfo.Name);
    }
}
