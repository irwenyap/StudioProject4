using UnityEngine;

public class CoinDrop : MonoBehaviour {
    [SerializeField]
    private GameObject CoinPouch;
    void Start() {
        _ = Instantiate(CoinPouch, transform.position, Quaternion.identity);
    }
}
