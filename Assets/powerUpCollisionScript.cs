using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpCollisionScript : MonoBehaviour
{

    CircleCollider2D cc;
    public LayerMask Player;
    public GameObject spawnref;
    private void Awake()
    {
        cc = gameObject.GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
