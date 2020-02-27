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
            int rand = Random.Range(0, 5);
            //0 = health , 1 = critchance , 2 = critdamage, 3 = armour , 4 = movespeed, 5 = attack speed , 6 = atk damage
            //Add Stats Here
            switch(rand)
            {
                case 0:
            collision.gameObject.GetComponent<PlayerController>().maxHealth += 20;
            collision.gameObject.GetComponent<PlayerController>().currHealth += 20;
                    break;
                case 1:
            collision.gameObject.GetComponent<PlayerController>().critChance += 5;
                    break;
                case 2:
            collision.gameObject.GetComponent<PlayerController>().critDamage += 10;
                    break;
                case 3:
            collision.gameObject.GetComponent<PlayerController>().armour += 1;
                    break;
                case 4:
                    collision.gameObject.GetComponent<PlayerController>().moveSpeed += 1;
                    break;
            }

            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
