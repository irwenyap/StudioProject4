using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpCollisionScript : MonoBehaviour
{

    CircleCollider2D cc;
    public LayerMask Player;
    public GameObject spawnref;
    private void Awake()
    {
        cc = gameObject.GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(cc.IsTouchingLayers(Player))
        {
            Instantiate(spawnref, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
