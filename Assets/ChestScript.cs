using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public KeyCode key;
    public GameObject item;
    public LayerMask player;

    private CircleCollider2D CCdetection;

    private void Awake() {
        CCdetection = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if(CCdetection.IsTouchingLayers(player) && Input.GetKeyDown(key)) {
            item.SetActive(true);
            Destroy(gameObject);
        }
    }
}
