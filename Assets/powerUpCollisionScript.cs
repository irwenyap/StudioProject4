using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpCollisionScript : MonoBehaviour
{

    private void Awake()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Add Stats Here
            //collision.gameObject.Getcompoment<PlayerController>().addStats;

            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
