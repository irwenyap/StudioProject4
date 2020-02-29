using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBtwAttack;
    public float attackSpeed;
    public Transform player;
    public Transform control;
    public LayerMask whatIsEnemies;
    public float damage;

    void Start()
    {
       

        timeBtwAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float meleeAtackrange = control.GetComponent<AiMeleeControl>().AttackRange;
        float distance = Vector2.Distance(this.transform.position ,player.transform.position);
        timeBtwAttack += Time.deltaTime;
        if(timeBtwAttack >= attackSpeed && distance < meleeAtackrange)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(this.transform.position , control.GetComponent<AiMeleeControl>().AttackRange);
            timeBtwAttack = 0;
            //player.GetComponent<PlayerController>().TakeDamage(damage);
           

        }
    }
}
