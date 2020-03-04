using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeProjectileOnHit : MonoBehaviour
{
    public Transform player;
    public AiRangedControl RangedAi;
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        RangedAi = GameObject.FindGameObjectWithTag("Rangedai").GetComponentInChildren<AiRangedControl>();
        damage = RangedAi.damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);

        }
    }
}
