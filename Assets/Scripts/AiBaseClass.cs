using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBaseClass : MonoBehaviour
{
    // Start is called before the first frame updatE
    public float moveSpeed;
    public float health;
    public Transform player;
    public float DetectRange;
    public float AiDirection;
    public float DecisionChangeTimer;
    public float DecisionValue;
    public float AttackRange;

    void Start()
    {
        health = 100;
        moveSpeed = 0.01f;
        DetectRange = 7;
        AiDirection = 0;
        DecisionChangeTimer = 0;
        DecisionValue = 0;
        AttackRange = 4;
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
