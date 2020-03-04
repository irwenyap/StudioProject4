using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{


    public Rigidbody2D bullet;
    public Transform player;
    // Private
    float shootBT = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<FireBoss>();
        player = gameObject.GetComponent<AiRangedControl>().player;
    }

    // Update is called once per frame
    void Update()
    {
       // shootBT += Time.deltaTime;
        
       // float DistanceAiNPlayer = Vector2.Distance(player.position, this.transform.position);
       //// AttackRange = 4;
       // if (shootBT >= 0.5f && DistanceAiNPlayer  < 4) {
       //     Rigidbody2D rb = Instantiate(bullet, transform.position + (transform.up * 0.5f), transform.rotation);
       //     rb.velocity = rb.gameObject.transform.up * 10;
       //     shootBT = 0f;
       // }
    }
    
  
}
