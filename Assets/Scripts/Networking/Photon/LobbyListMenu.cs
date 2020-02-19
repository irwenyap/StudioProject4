using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyListMenu : MonoBehaviourPunCallbacks {
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private RoomList _lobbyListing;

    private List<RoomList> _listings = new List<RoomList>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        foreach (RoomInfo info in roomList) {
            if (info.RemovedFromList) {
                int index = _listings.FindIndex(x => x._roomInfo.Name == info.Name);
                if (index != -1) {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else {
                Debug.Log("Lobby Found");
                RoomList list = Instantiate(_lobbyListing, _content);
                if (list != null) {
                    _lobbyListing.SetRoomInfo(info);
                    _listings.Add(list);
                }
            }
        }
    }
}
