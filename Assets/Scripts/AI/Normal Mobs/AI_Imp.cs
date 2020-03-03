using UnityEngine;

public class AI_Imp : AI_Base {
    private CircleCollider2D myCircleCollider;

    void Start() {
        myCircleCollider = GetComponent<CircleCollider2D>();

        // Stats
        maxHealth = 50f;
        currHealth = maxHealth;
        moveSpeed = 5f;
    }

    void Update() {
        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 2 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 8) {
            //collision.gameObject.GetComponent<PlayerController>().TakeDamage(10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            target = collision.transform;
            myCircleCollider.enabled = false; // error
        }
    }
}
