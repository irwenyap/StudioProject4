using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBoss  : AiBaseClass
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 1200;
        currHealth = maxHealth;
        moveSpeed = 0.01f;
        DetectRange = 10;
        AiDirection = 0;
        DecisionChangeTimer = 0;
        DecisionValue = 0;
        AttackRange = 2;
        damage = 49;
        attackSpeed = 15;
    }
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        DecisionChangeTimer += Time.deltaTime;
        float DistanceAiNPlayer = Vector2.Distance(player.position, this.transform.position);
        if (DistanceAiNPlayer > DetectRange)//Idle
        {
            animator.SetBool("EarthBossChase", false);
            animator.SetBool("EarthBossAttack", false);
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
            transform.rotation = Quaternion.AngleAxis(AiDirection, Vector3.forward);
        }
        if (DistanceAiNPlayer < DetectRange)//Chase
        {
            animator.SetBool("EarthBossChase", true);
            animator.SetBool("EarthBossAttack", false);
            Vector2 direction = player.position - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        }
        if (DistanceAiNPlayer < DetectRange && DistanceAiNPlayer > AttackRange)//Chase
        {
            animator.SetBool("EarthBossChase", true);
            animator.SetBool("EarthBossAttack", false);
            this.transform.Translate(moveSpeed, 0, 0);

        }
        else if (DistanceAiNPlayer < AttackRange)//Attack
        {
            animator.SetBool("EarthBossChase", false);
            animator.SetBool("EarthBossAttack", true);
        }
    }
}
