using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStuffWitWeight : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Objects;
    public int random;
    private int[] weights = {
        44,
        20,
        15,
        15,
        5,
        1
    };  //this need to crospond with the objects array

    void Start() {
        int rand = Random.Range(0, 100);
        random = rand;
        for (int i = 0; i < weights.Length;++i) {
            if(random < weights[i]) {
                GameObject go = Instantiate(Objects[i], transform.position, Quaternion.identity);
                go.transform.parent = transform;
                break;
            }
            random -= weights[i]; 
        }
    }
}
