using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRifleBullet : ProjectileBase {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 10) {
            collision.gameObject.GetComponent<AI_Base>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
