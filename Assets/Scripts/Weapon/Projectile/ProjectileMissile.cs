using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMissile : ProjectileBase {
    [SerializeField]
    private Transform target;

    [SerializeField]
    private GameObject collisionEffect;

    private CircleCollider2D myCircleCollider;

    private void Start() {
        myCircleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update() {
        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 2 * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, (transform.position - target.position) - new Vector3(0, 0, 90));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        _ = Instantiate(collisionEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        //if (collision.gameObject.tag == "Player") {
            target = collision.transform;
            Destroy(myCircleCollider);
        //}
    }

}
