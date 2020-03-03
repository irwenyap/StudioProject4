using UnityEngine;

public class ProjectileCommon : ProjectileBase {
    [SerializeField]
    private GameObject explodeEffect;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 10) {
            collision.gameObject.GetComponent<AI_Base>().TakeDamage(damage);
        }
        if (explodeEffect)
            _ = Instantiate(explodeEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
