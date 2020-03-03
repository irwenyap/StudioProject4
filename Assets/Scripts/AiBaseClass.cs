using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBaseClass : MonoBehaviour {

    protected float maxHealth;
    protected float currHealth;
    protected float moveSpeed;

    public Transform player;
    public float DetectRange;
    public float AiDirection;
    public float DecisionChangeTimer;
    public float DecisionValue;
    public float AttackRange;
    public int damage;
    public float attackSpeed;
    public float attackTimer;

    void Start() {
        moveSpeed = 0.01f;
        DetectRange = 7;
        AiDirection = 0;
        DecisionChangeTimer = 0;
        DecisionValue = 0;
        AttackRange = 4;
        attackSpeed = 2;
        damage = 2;
        attackTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            player = collision.transform;
        }
    }
}
