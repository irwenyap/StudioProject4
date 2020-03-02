using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Base : MonoBehaviour {
    protected float maxHealth;
    public float currHealth;
    protected float moveSpeed;

    public Transform target;
    public AI_Healthbar healthbar;

    public void TakeDamage(float damage) {
        currHealth -= damage;
        healthbar.SetHealth(currHealth / maxHealth);
        if (currHealth <= 0)
            Destroy(gameObject);
    }
}
