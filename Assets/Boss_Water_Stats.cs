using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Water_Stats :AI_Base
{
    public PlayerList player;

    public BoxCollider2D damagePlayer;
    private bool boosactive;
    Animator myanimator;
    public GameObject bulletRef;
    public bool healed;
    public float rehealTimer;
    public GameObject bossloot;
    private void Awake()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Player")
        {
          collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Vector2 direction = (collision.gameObject.transform.position -gameObject.transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction*1000);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        maxHealth = DifficultySystem.difficulty * 10000;
        currHealth = maxHealth;
        damage = 2*DifficultySystem.difficulty;
        moveSpeed = 3;
        weight = 1;
        damagePlayer = gameObject.GetComponent<BoxCollider2D>();
        boosactive = false;
        myanimator = gameObject.GetComponent<Animator>();
        healed = false;
        rehealTimer = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(currHealth < maxHealth && !boosactive)
        {
            boosactive = true;
            myanimator.SetBool("active", true);
        }
        if(currHealth<= maxHealth*0.5f && !healed)
        {
            myanimator.SetBool("IsPhase2", true);

        }
        else if(rehealTimer <= 0 && healed)
        {
            healed = false;
            rehealTimer = 30.0f;
        }
        else if(healed)
        {
            myanimator.SetBool("IsPhase2", false);
            rehealTimer -= Time.deltaTime;

        }
        if(currHealth <0)
        {
            //dead
            _ = Instantiate(bossloot, transform.position, Quaternion.identity);
            myanimator.SetBool("IsDead", true);

        }
    }
}
