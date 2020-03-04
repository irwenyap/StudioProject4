using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirProjectileOnHit : AirBoss
{
    public Transform player;
    public AirBoss airboss;
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        airboss = GameObject.FindGameObjectWithTag("Airboss").GetComponentInChildren<AirBoss>();
        damage = airboss.damage;
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
