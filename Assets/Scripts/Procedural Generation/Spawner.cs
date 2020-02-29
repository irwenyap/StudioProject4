using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Objects;
    bool SpawnerActivate = false;
    

    // Start is called before the first frame update
    void Start()
    {
            for (int i = 0; i <= 2; ++i)
            {
                int rand = Random.Range(0, Objects.Length);
                if (Objects[rand] != null)
                {
                    GameObject instance = (GameObject)Instantiate(Objects[rand], transform.position, Quaternion.identity);
                    instance.transform.parent = transform.parent;
                }
            }
        Destroy(gameObject);
    }
}
