using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    public GameObject[] Spawner;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (GameObject go in Spawner) {
                go.SetActive(true);
            }
            Destroy(gameObject);
            Debug.Log("calledwhentrigggers");
        }
    }
}
