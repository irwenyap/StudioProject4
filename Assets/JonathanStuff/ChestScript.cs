using UnityEngine;

public class ChestScript : MonoBehaviour {
    //public LayerMask player;
    //private CircleCollider2D CCdetection;
    [SerializeField]
    private GameObject item;
    [SerializeField]
    private int cost;
    //private void Awake() {
    //    CCdetection = gameObject.GetComponent<CircleCollider2D>();
    //}

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (collision.gameObject.GetComponent<PlayerController>().coins >= cost) {
                    collision.gameObject.GetComponent<PlayerController>().UseCoins(cost);
                    item.SetActive(true);
                    Destroy(gameObject);
                }
            }
        }
    }
}