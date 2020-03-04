using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMeleeControl : AI_Base {
    private Animator animator;
    [SerializeField]
    private CircleCollider2D myCircleCollider;

    Vector2 moveAxis;
    Vector2 mousePos;

    float AiDirection;
    float DecisionChangeTimer;
    float DecisionValue;
    float AttackRange;

    // Start is called before the first frame update
    void Start() {
        maxHealth = 100;
        currHealth = maxHealth;
        moveSpeed = 5f;
        AiDirection = 0;
        DecisionChangeTimer = 0;
        DecisionValue = 0;
        AttackRange = 2;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        //DecisionChangeTimer += Time.deltaTime;
        if (target) //Idle
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 2 * Time.deltaTime);

            //animator.SetBool("Chase", false);
            //animator.SetBool("Attack", false);

            //if (DecisionChangeTimer > 3) {
            //    DecisionValue = Random.Range(0, 7);
            //    DecisionChangeTimer = 0;
            //}
            //switch (DecisionValue) {
            //    case 0:
            //        AiDirection = 45;
            //        break;
            //    case 1:
            //        AiDirection = 90;
            //        break;
            //    case 2:
            //        AiDirection = 135;
            //        break;
            //    case 3:
            //        AiDirection = 180;
            //        break;
            //    case 4:
            //        AiDirection = 225;
            //        break;
            //    case 5:
            //        AiDirection = 270;
            //        break;
            //    case 6:
            //        AiDirection = 315;
            //        break;
            //    case 7:
            //        AiDirection = 360;
            //        break;
            //}
            //transform.rotation = Quaternion.AngleAxis(AiDirection, Vector3.forward);
        }
        //else {
        //animator.SetBool("Chase", true);
        //animator.SetBool("Attack", false);
        //Vector2 direction = target.position - transform.position;

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        //var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //transform.Translate(moveSpeed, 0, 0);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 8) {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            target = collision.transform;
            myCircleCollider.enabled = false; // error
        }
    }
}
