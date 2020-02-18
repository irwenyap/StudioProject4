using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private RoomList _roomListing;

    private List<RoomList> _listings = new List<RoomList>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        foreach (RoomInfo info in roomList) {
            if (info.RemovedFromList) {
                int index = _listings.FindIndex(x => x._roomInfo.Name == info.Name);
                if (index != -1) {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            } else {
                RoomList list = Instantiate(_roomListing, _content);
                if (list != null) {
                    _roomListing.SetRoomInfo(info);
                    _listings.Add(list);
                }
            }
        }
    }
}
