using Photon.Pun;
using UnityEngine;

public class SpawnItem : MonoBehaviour {
    [SerializeField]
    private GameObject[] Objects;

    void Start() {
        int rand = Random.Range(0, Objects.Length);
        if (Objects[rand] != null) {
            //GameObject instance = (GameObject)Instantiate(Objects[rand], transform.position, Quaternion.identity);
            if (PhotonNetwork.IsMasterClient)
                _ = PhotonNetwork.InstantiateSceneObject(Objects[rand].GetComponent<WeaponBase>().prefabPath, transform.position, Quaternion.identity);
        }
    }
}
