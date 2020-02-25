using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListMenu : MonoBehaviourPunCallbacks
{
    public List<Text> playerText;
    private List<PlayerListing> players = new List<PlayerListing>();
    private List<PlayerListing> reOrgPlayers = new List<PlayerListing>();

    private bool isReady = false;

    //private List<PlayerListing> _listings = new List<PlayerListing>();

    private void Awake() {
        RetrieveLobbyInfo();
    }

    public override void OnEnable() {
        base.OnEnable();
        SetReadyUp(false);
    }

    private void SetReadyUp(bool state) {
        isReady = state;
    }

    private void RetrieveLobbyInfo() {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players) {
            PlayerListing temp = new PlayerListing();
            temp.SetPlayerInfo(playerInfo.Value);
            players.Add(temp);
        }
        SortPlayers();
        DisplayPlayersInLobby();
    }

    private void SortPlayers() {
        for (int i = 1; i < players.Count; ++i) {
            reOrgPlayers.Add(players[i]);
        }
        reOrgPlayers.Add(players[0]);
    }

    private void DisplayPlayersInLobby() {
        for (int j = 0; j < reOrgPlayers.Count; ++j) {
            for (int i = 0; i < 4; ++i) {
                if (playerText[i].text == "") {
                    playerText[i].text = reOrgPlayers[j]._player.NickName;
                    break;
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        for (int i = 0; i < 4; ++i) {
            if (playerText[i].text == "") {
                playerText[i].text = newPlayer.NickName;
                break;
            }
        }
        PlayerListing temp = new PlayerListing();
        temp._player = newPlayer;
        reOrgPlayers.Add(temp);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        
    }

    public void EnterGame() {
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void ReadyUp() {
        SetReadyUp(!isReady);
        base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.All, PhotonNetwork.LocalPlayer, isReady);
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready) {
        int index = reOrgPlayers.FindIndex(x => x._player == player);
        if (index != -1) {
            reOrgPlayers[index].isReady = ready;
            playerText[index].transform.Find("Ready").GetComponent<Text>().color = new Color(0, 1, 0);
        }
    }
}
