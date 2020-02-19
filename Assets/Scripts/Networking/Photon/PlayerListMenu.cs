using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private PlayerListing _playerListing;

    public Text player01;
    public Text player02;
    public Text player03;
    public Text player04;

    private List<PlayerListing> _listings = new List<PlayerListing>();

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        if (player01.text == "") {
            player01.text = newPlayer.NickName;
        } else if (player02.text == "") {
            Debug.Log("Player 2 has joined");
            player02.text = newPlayer.NickName;
        } else if (player03.text == "") {
            Debug.Log("Player 3 has joined");
            player03.text = newPlayer.NickName;
        } else if (player04.text == "") {
            player04.text = newPlayer.NickName;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        
    }

    //public override void OnRoomListUpdate(List<RoomInfo> roomList) {
    //    foreach (RoomInfo info in roomList) {
    //        if (info.RemovedFromList) {
    //            int index = _listings.FindIndex(x => x._roomInfo.Name == info.Name);
    //            if (index != -1) {
    //                Destroy(_listings[index].gameObject);
    //                _listings.RemoveAt(index);
    //            }
    //        }
    //        else {
    //            RoomList list = Instantiate(_roomListing, _content);
    //            if (list != null) {
    //                _roomListing.SetRoomInfo(info);
    //                _listings.Add(list);
    //            }
    //        }
    //    }
    //}
}
