using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Base : MonoBehaviour {
    protected float maxHealth;
    protected float currHealth;
    protected float moveSpeed;

    public Transform target;

    public void TakeDamage(float damage) {
        currHealth -= damage;
        if (currHealth <= 0)
            Destroy(gameObject);
    }
}
