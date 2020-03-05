using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkullshot : ProjectileBase {
    [SerializeField]
    private GameObject explodeEffect;
    private PlaySound mySound;

    private void Awake() {
        mySound = GetComponent<PlaySound>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 8) {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(75);
        }
        if (explodeEffect) {
            _ = Instantiate(explodeEffect, gameObject.transform.position, Quaternion.identity);
            Camera.main.GetComponent<ShakeBehaviour>().TriggerShake(1f);
        }
        mySound.ApplySound();
        Destroy(gameObject);
    }
}
