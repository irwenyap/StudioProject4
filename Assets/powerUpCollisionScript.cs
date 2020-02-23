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
            GameObject go = Instantiate(spawnref, transform.position, Quaternion.identity);
            go.transform.parent = transform.parent;
            Destroy(gameObject);
        }
    }
}
