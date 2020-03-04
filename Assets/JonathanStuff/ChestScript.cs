using Photon.Pun;
using UnityEngine;

public class ChestScript : ChestBase {
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (collision.gameObject.GetComponent<PlayerController>().coins >= cost) {
                    collision.gameObject.GetComponent<PlayerController>().UseCoins(cost);
                    photonView.RPC("RPC_OpenChest", RpcTarget.All);
                }
            }
        }
    }
}   