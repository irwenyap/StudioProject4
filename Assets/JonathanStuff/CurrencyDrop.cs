using Photon.Pun;
using UnityEngine;

public class CurrencyDrop : MonoBehaviourPun {
    public int amount;

    void Start() {
        amount = Random.Range(100, 200);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            if (collision.gameObject.GetComponent<PlayerController>().enabled)
                collision.gameObject.GetComponent<PlayerController>().CollectCoins(amount);
            Destroy(gameObject);
        }
    }
}
