using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AiRangedControl: MonoBehaviour
{
    // Public
    public float moveSpeed = 0.01f;
   // public Rigidbody2D bullet;
    // Private
    Rigidbody2D myRigidbody;
    Vector2 moveAxis;
    Vector2 mousePos;
    float health = 100f;
    public Transform player;
    float shootBT = 0f;
    float RangeDetectRange = 7;
    float RangeAttackRange = 4;
    float DecisionChangeTimer;
    float DecisionValue;
    float AiDirection;
    private Animator animator;


    private void Start()
    {

        //myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        shootBT += Time.deltaTime;
        DecisionChangeTimer += Time.deltaTime;
        float DistanceAiNPlayer = Vector2.Distance(player.position, this.transform.position);
        if (DistanceAiNPlayer > RangeDetectRange)
        {
            animator.SetBool("RangedChase", false);
            animator.SetBool("RangedAttack", false);
            this.transform.Translate(moveSpeed, 0, 0);

            if (DecisionChangeTimer > 3)
            {
                DecisionValue = Random.Range(0, 7);
                DecisionChangeTimer = 0;
            }
            switch (DecisionValue)
            {
                case 0:
                    AiDirection = 45;
                    break;
                case 1:
                    AiDirection = 90;
                    break;
                case 2:
                    AiDirection = 135;
                    break;
                case 3:
                    AiDirection = 180;
                    break;
                case 4:
                    AiDirection = 225;
                    break;
                case 5:
                    AiDirection = 270;
                    break;
                case 6:
                    AiDirection = 315;
                    break;
                case 7:
                    AiDirection = 360;
                    break;
            }
          //  if (transform.rotation != Quaternion.AngleAxis(AiDirection, Vector3.forward))
          //  {
                transform.rotation = Quaternion.AngleAxis(AiDirection, Vector3.forward);
         //   }
        }
        if (DistanceAiNPlayer < RangeDetectRange)
        {
            animator.SetBool("RangedChase", true);
            animator.SetBool("RangedAttack", false);
            Vector2 direction = player.position - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (DistanceAiNPlayer < RangeDetectRange && DistanceAiNPlayer > RangeAttackRange)
        {
            animator.SetBool("RangedChase", true);
            animator.SetBool("RangedAttack", false);
            this.transform.Translate(moveSpeed, 0, 0);
        }
        else if (DistanceAiNPlayer < RangeAttackRange)
        {
            animator.SetBool("RangedChase", false);
            animator.SetBool("RangedAttack", true);

        }

        // Cursor Follow
        //mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.z, Camera.main.transform.position.z - transform.position.z));
        //transform.LookAt(mousePos);
        //var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);


    }

    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + moveAxis * moveSpeed * Time.deltaTime);
    }
}
