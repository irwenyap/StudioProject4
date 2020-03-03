using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Objects;
    bool SpawnerActivate = false;

    void Start() {
        for (int i = 0; i <= 2; ++i) {
            int rand = Random.Range(0, Objects.Length);
            if (Objects[rand] != null) {
                //_ = PhotonNetwork.Instantiate("Prefabs/AIs/Imp", transform.position, Quaternion.identity);
                _ = PhotonNetwork.InstantiateSceneObject("Prefabs/AIs/Imp", transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
