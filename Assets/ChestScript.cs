using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public KeyCode key;
    public GameObject item;
    public LayerMask player;
    public int amountToOpen;
    private CircleCollider2D CCdetection;

    private void Awake() {
        CCdetection = gameObject.GetComponent<CircleCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(key) && collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerController>().currency >= amountToOpen)
        {
            collision.gameObject.GetComponent<PlayerController>().currency -= amountToOpen;
            item.SetActive(true);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
    }
}