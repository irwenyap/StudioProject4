using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePiercingArrow : MonoBehaviour {
    private int enemyPierced = 0;
    private int maxPiercing = 2;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 10) {
            ++enemyPierced;
            if (enemyPierced >= maxPiercing)
                Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
