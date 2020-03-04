using UnityEngine;

public class EarthProjectileOnHit : MonoBehaviour
{
    public Transform player;
    public EarthBoss earthboss;
    private int damage;
    private int originSpeed;
    private float speedebuff;
    private float debufftimer;
    private float debufftimercount;
    private bool debuffOn;      
    // Start is called before the first frame update
    void Start()
    {
        earthboss = GameObject.FindGameObjectWithTag("Earthboss").GetComponentInChildren<EarthBoss>();
        speedebuff = 0f;
        debuffOn = false;
        damage = earthboss.damage;
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
            
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(49);
            debuffOn = true;
            Destroy(gameObject);

        }
    }
    
}
