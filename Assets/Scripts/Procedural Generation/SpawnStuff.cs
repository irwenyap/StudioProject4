using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStuff : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Objects;

    

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, Objects.Length);
        if (Objects[rand] != null) {
            GameObject instance = (GameObject)Instantiate(Objects[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
    }
}
