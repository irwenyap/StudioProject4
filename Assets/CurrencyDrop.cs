using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyDrop : MonoBehaviour
{
    public int amount;
    // Start is called before the first frame update
    void Start()
    {
        amount = Random.Range(10, 20);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().currency += amount;
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
