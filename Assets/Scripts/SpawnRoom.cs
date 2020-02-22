using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask room;

    public LevelGen gen;
    // Update is called once per frame
    void Update()
    {
        //Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
        //if(roomDetection == null && gen.stopGeneration)
        //{
        //    int rand =Random.Range(0, gen.G_C_A_rooms.Length);
        //    Instantiate(gen.G_C_A_rooms[rand], transform.position, Quaternion.identity);
        //    Destroy(gameObject);
        //}

    }
}
