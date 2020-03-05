using Photon.Pun;
using UnityEngine;

public class SpawnStuffWitWeight : MonoBehaviour {
    [SerializeField]
    private GameObject[] Objects;
    public int random;
    private int[] weights = {
        46,
        22,
        16,
        16
    };  //this need to crospond with the objects array

    void Start() {
        int rand = Random.Range(0, 100);
        random = rand;
        for (int i = 0; i < weights.Length; ++i) {
            if (random < weights[i]) {
                if (random <= 46) {
                    _ = Instantiate(Objects[i], transform.position, Quaternion.identity);
                    break;
                }
                else {
                    //GameObject go = Instantiate(Objects[i].GetComponent<ChestBase>().prefabPath, transform.position, Quaternion.identity);
                    GameObject chest = PhotonNetwork.InstantiateSceneObject(Objects[i].GetComponent<ChestBase>().prefabPath, transform.position, Quaternion.identity);
                    chest.transform.parent = transform;
                    break;
                }
            }
            random -= weights[i];
        }
    }
}
