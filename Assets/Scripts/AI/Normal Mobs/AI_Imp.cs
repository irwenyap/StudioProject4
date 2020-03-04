using UnityEngine;

public class AI_Imp : AI_Base {
    private CircleCollider2D detectionRange;

    void Start() {
        detectionRange = GetComponent<CircleCollider2D>();

        // Stats
        maxHealth = 50f;
        currHealth = maxHealth;
        moveSpeed = 5f;
        damage = 5;
        weight = 1;
    }

    void Update() {
        if (target) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 2 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 8) {
            if (collision.gameObject.GetComponent<PlayerController>().enabled)
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            target = collision.transform;
            detectionRange.enabled = false; // error
        }
    }
}
