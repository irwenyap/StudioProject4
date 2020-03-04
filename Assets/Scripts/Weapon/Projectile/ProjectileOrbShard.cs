using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOrbShard : ProjectileBase
{
    // Start is called before the first frame update
    void Start() {
        damage = 10;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 8) {
            //collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
