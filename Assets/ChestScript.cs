using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D CCdetection;

    public KeyCode key;

    public GameObject thingInside;

    public LayerMask player;

    private void Awake()
    {
        CCdetection = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CCdetection.IsTouchingLayers(player) && Input.GetKeyDown(key))
        {
            thingInside.SetActive(true);
            Destroy(gameObject);
        }
    }
}
