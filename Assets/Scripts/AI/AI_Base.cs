using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Base : MonoBehaviour {
    public float maxHealth;
    public float currHealth;
    public float moveSpeed;
    public int damage;

    public Transform target;
    public AI_Healthbar healthbar;

    public string prefabPath;
    public int weight;

    public void TakeDamage(float damage) {
        currHealth -= damage;
        healthbar.SetHealth(currHealth / maxHealth);
        if (currHealth <= 0)
            Destroy(gameObject);
    }
}
