using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpawnTrigger;
    public GameObject[] Spawner;
    void Start()
    {
        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (GameObject go in Spawner)
            go.SetActive(true);
            SpawnTrigger.SetActive(false);
            Debug.Log("calledwhentrigggers");
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
