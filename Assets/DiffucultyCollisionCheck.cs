using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffucultyCollisionCheck : MonoBehaviour
{
 
    private void Awake()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag== "Player")
        {
            DifficultySystem.roomEntered++;
            Destroy(gameObject);
        }
    }
}
