using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbiterOcram_PhaseHalo : MonoBehaviour {
    float x, y;

    void Start() {
        x = y = 1;
    }

    void Update() {
        x += Time.deltaTime * 20;
        y += Time.deltaTime * 20;
        transform.localScale = new Vector3(x, y);

        if (transform.localScale.x >= 20)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            collision.attachedRigidbody.AddForce(dir * 2500);
            collision.GetComponent<PlayerController>().TakeDamage(40);
        }
    }
}
