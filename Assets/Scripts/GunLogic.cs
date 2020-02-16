using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    public Rigidbody2D bullet;

    // Private
    float shootBT = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootBT += Time.deltaTime;
        if (Input.GetMouseButton(0) && shootBT >= 0.5f) {
            Rigidbody2D rb = Instantiate(bullet, transform.position + (transform.up * 0.5f), transform.rotation);
            rb.velocity = rb.gameObject.transform.up * 10;
            shootBT = 0f;
        }
    }
}
