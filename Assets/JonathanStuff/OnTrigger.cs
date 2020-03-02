using UnityEngine;

public class OnTrigger : MonoBehaviour {
    public GameObject[] Spawner;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            GetComponent<Collider2D>().enabled = false;
            foreach (GameObject go in Spawner) {
                if (go)
                    go.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
