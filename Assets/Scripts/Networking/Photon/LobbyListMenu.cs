using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyListMenu : MonoBehaviourPunCallbacks {
    [SerializeField]
    private Transform _content = null;
    [SerializeField]
    private LobbyListing _lobbyListing = null;

    private List<LobbyListing> _listings = new List<LobbyListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        foreach (RoomInfo info in roomList) {
            if (info.RemovedFromList) {
                int index = _listings.FindIndex(x => x.myRoomInfo.Name == info.Name);
                if (index != -1) {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else {
                Debug.Log("Lobby Found");
                LobbyListing list = Instantiate(_lobbyListing, _content);
                if (list != null) {
                    _lobbyListing.SetRoomInfo(info);
                    _listings.Add(list);
                }
            }
        }
    }
}
