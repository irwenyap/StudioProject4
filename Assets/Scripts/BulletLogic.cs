using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public GameObject explodeEffect;
    private void OnCollisionEnter2D(Collision2D collision) {
        //gameObject.SetActive(false);
        _ = Instantiate(explodeEffect, gameObject.transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
