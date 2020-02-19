using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    private Button myButton;

    public RoomInfo myRoomInfo;

    private OnlineLobbyMenu myParent;

    private void Start() {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(JoinLobby);
        myParent = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<OnlineLobbyMenu>();
    }

    public void SetRoomInfo(RoomInfo roomInfo) {
        myRoomInfo = roomInfo;
        _text.text = roomInfo.Name;
    }

    public void JoinLobby() {
        Debug.Log("Joining Lobby");
        PhotonNetwork.JoinRoom("Test");
        myParent.AcceptButton();
    }
}
