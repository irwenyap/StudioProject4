using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : AiBaseClass
{
    private Animator animator;
    public Transform weapon1;
    public Transform weapon2;
    public Transform weapon3;

    //RingOfFire
    public Transform weapon4;
    public Transform weapon5;
    public Transform weapon6;
    public Transform weapon7;
    public Transform weapon8;
    public Transform weapon9;
    public Rigidbody2D bullet;
    public Rigidbody2D bullet2;
    private float RingOfFireCooldown;
    private float RingOfFireCountdown;
    private float projectileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 750;
        currHealth = maxHealth;
        moveSpeed = 0.01f;
        DetectRange = 10;
        AiDirection = 0;
        DecisionChangeTimer = 0;
        DecisionValue = 0;
        AttackRange = 7;
        damage = 20;
        attackSpeed = 0.8f;
        RingOfFireCooldown = 2;

    }
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        RingOfFireCountdown += Time.deltaTime;
        DecisionChangeTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        float DistanceAiNPlayer = Vector2.Distance(player.position, this.transform.position);
        if (DistanceAiNPlayer > DetectRange)//Idle
        {
            animator.SetBool("FireBossChase", false);
            animator.SetBool("FireBossAttack", false);
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
            animator.SetBool("FireBossChase", true);
            animator.SetBool("FireBossAttack", false);
            Vector2 direction = player.position - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        }
        if (DistanceAiNPlayer < DetectRange && DistanceAiNPlayer > AttackRange)//Chase
        {
            animator.SetBool("FireBossChase", true);
            animator.SetBool("FireBossAttack", false);
            this.transform.Translate(moveSpeed, 0, 0);

        }
        else if (DistanceAiNPlayer < AttackRange)//Attack
        {
            if (attackTimer >= attackSpeed)
            {
                Rigidbody2D rb = Instantiate(bullet, weapon1.transform.position + (weapon1.transform.up * 0.5f), weapon1.transform.rotation);
                rb.velocity = rb.gameObject.transform.up * 10;

                Rigidbody2D rb1 = Instantiate(bullet, weapon2.transform.position + (weapon2.transform.up * 0.5f), weapon2.transform.rotation);
                rb1.velocity = rb1.gameObject.transform.up * 10;

                Rigidbody2D rb2 = Instantiate(bullet, weapon3.transform.position + (weapon3.transform.up * 0.5f), weapon3.transform.rotation);
                rb2.velocity = rb2.gameObject.transform.up * 10;
                attackTimer = 0f;
            }
            animator.SetBool("FireBossChase", false);
            animator.SetBool("FireBossAttack", true);
        }
        if(RingOfFireCountdown > RingOfFireCooldown)
        {
            Rigidbody2D rb3 = Instantiate(bullet2, weapon3.transform.position + (weapon3.transform.up * 0.5f), weapon3.transform.rotation);
            rb3.velocity = rb3.gameObject.transform.up * 10;
            Rigidbody2D rb31 = Instantiate(bullet2, weapon3.transform.position + (weapon3.transform.up * -0.5f), weapon3.transform.rotation);
            rb31.velocity = rb31.gameObject.transform.up * -10;


            Rigidbody2D rb4 = Instantiate(bullet2, weapon4.transform.position + (weapon4.transform.up * 0.5f), weapon4.transform.rotation);
            rb4.velocity = rb4.gameObject.transform.up * 10;
            Rigidbody2D rb41 = Instantiate(bullet2, weapon3.transform.position + (weapon3.transform.up * -0.5f), weapon3.transform.rotation);
            rb41.velocity = rb41.gameObject.transform.up * -10;


            Rigidbody2D rb5 = Instantiate(bullet2, weapon5.transform.position + (weapon5.transform.up * 0.5f), weapon5.transform.rotation);
            rb5.velocity = rb5.gameObject.transform.up * 10;
            Rigidbody2D rb51 = Instantiate(bullet2, weapon5.transform.position + (weapon5.transform.up * -0.5f), weapon5.transform.rotation);
            rb51.velocity = rb51.gameObject.transform.up* -10;
            

            Rigidbody2D rb6 = Instantiate(bullet2, weapon6.transform.position + (weapon6.transform.up * 0.5f), weapon6.transform.rotation);
            rb6.velocity = rb6.gameObject.transform.up * 10;
            Rigidbody2D rb61 = Instantiate(bullet2, weapon6.transform.position + (weapon6.transform.up * -0.5f), weapon6.transform.rotation);
            rb61.velocity = rb61.gameObject.transform.up * -10;


            Rigidbody2D rb7 = Instantiate(bullet2, weapon7.transform.position + (weapon7.transform.up * 0.5f), weapon7.transform.rotation);
            rb7.velocity = rb7.gameObject.transform.up * 10; 
            Rigidbody2D rb71 = Instantiate(bullet2, weapon7.transform.position + (weapon7.transform.up * -0.5f), weapon7.transform.rotation);
            rb71.velocity = rb71.gameObject.transform.up * -10;


            Rigidbody2D rb8 = Instantiate(bullet2, weapon8.transform.position + (weapon8.transform.up * 0.5f), weapon8.transform.rotation);
            rb8.velocity = rb8.gameObject.transform.up * 10;
            Rigidbody2D rb81 = Instantiate(bullet2, weapon8.transform.position + (weapon8.transform.up * -0.5f), weapon8.transform.rotation);
            rb81.velocity = rb81.gameObject.transform.up * -10;


            Rigidbody2D rb9 = Instantiate(bullet2, weapon9.transform.position + (weapon9.transform.up * 0.5f), weapon9.transform.rotation);
            rb9.velocity = rb9.gameObject.transform.up * 10; 
            Rigidbody2D rb91 = Instantiate(bullet2, weapon9.transform.position + (weapon9.transform.up * -0.5f), weapon9.transform.rotation);
            rb91.velocity = rb91.gameObject.transform.up * -10;
          
            RingOfFireCountdown = 0;
            

        }
    }
}
