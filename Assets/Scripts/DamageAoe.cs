using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAoe : MonoBehaviour
{
   public int damage;
    private void Awake()
    {
        damage = 10;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<AI_Base>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
