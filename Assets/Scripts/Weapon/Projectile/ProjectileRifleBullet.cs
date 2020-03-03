using UnityEngine;

public class ProjectileRifleBullet : ProjectileBase {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 10) {
            collision.gameObject.GetComponent<AI_Base>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
