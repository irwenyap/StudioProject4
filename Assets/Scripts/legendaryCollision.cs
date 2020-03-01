using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legendaryCollision : MonoBehaviour
{
    public int type ;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {   
            if(type == 1)
            collision.gameObject.AddComponent<fire_Legendary_item>();
            else if(type == 2)
            collision.gameObject.AddComponent<Earth_LegendaryItem>();
            else if(type == 3)
            collision.gameObject.AddComponent<Air_LegendaryItem>();
            else if(type == 4)
            collision.gameObject.AddComponent<Water_Legendaryitem>();

            Destroy(gameObject);
        }
    }
}
