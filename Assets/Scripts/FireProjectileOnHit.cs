using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileOnHit : MonoBehaviour
{
    public Transform player;
    public FireBoss fireboss;
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        fireboss = GameObject.FindGameObjectWithTag("Fireboss").GetComponentInChildren<FireBoss>();
        damage = fireboss.damage;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
            
        }
    }
   

}
