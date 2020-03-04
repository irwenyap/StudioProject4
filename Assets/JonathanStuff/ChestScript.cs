using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ChestScript : ChestBase {
    [SerializeField]
    private GameObject costUI;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {

            if (costUI)
                LeanTween.scale(costUI, new Vector3(0.8375f, 1, 1), Time.deltaTime * 10).setEaseInOutQuint();

            if (Input.GetKeyDown(KeyCode.E)) {
                if (collision.gameObject.GetComponent<PlayerController>().coins >= cost) {
                    collision.gameObject.GetComponent<PlayerController>().UseCoins(cost);
                    photonView.RPC("RPC_OpenChest", RpcTarget.All);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == 8)
            if (costUI)
                LeanTween.scale(costUI, new Vector3(0, 0, 0), Time.deltaTime * 10).setEaseInOutQuint();
    }


    //private void OpenCostUI() {
        
    //    StartCoroutine("CloseCostUI");
    //}

    //IEnumerator CloseCostUI() {
    //    yield return new WaitForSeconds(2);
    //}
}   