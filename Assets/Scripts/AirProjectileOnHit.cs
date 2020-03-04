using UnityEngine;

public class AirProjectileOnHit : MonoBehaviour
{
    public Transform player;
    public AirBoss airboss;
    private int damage;
    private int originSpeed;
    private float speedebuff;
    private float debufftimer;
    private float debufftimercount;
    private bool debuffOn;      
    // Start is called before the first frame update
    void Start()
    {
        airboss = GameObject.FindGameObjectWithTag("Airboss").GetComponentInChildren<AirBoss>();
        speedebuff = 3.5f;
        debuffOn = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(debuffOn == true)
        {
            GetComponent<PlayerController>().moveSpeed = speedebuff;
            debufftimercount += Time.deltaTime;
            if(debufftimercount> debufftimer)
            {
                debuffOn = false;
            }
        }
        if (debuffOn == false)
        {
            GetComponent<PlayerController>().moveSpeed = 5; 
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            debuffOn = true;
            Destroy(gameObject);

        }
    }
    
}
