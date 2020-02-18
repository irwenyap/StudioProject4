﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    enum Direction
    {
        D_UP =0,
        D_DOWN,
        D_LEFT,
        D_RIGHT
    }

    Direction E_direction;
    int I_direction;
    [SerializeField]
    private Transform[] T_C_startPos;
    [SerializeField]
    public GameObject[] G_C_A_rooms;
    // index 0 == LR Room, index 1 == LRD Room, index 2 == LRU Room ,index 3 == LRUD Room
    [SerializeField]
    public float F_roomDIfferent;
    [SerializeField]
    private float F_timeBtwnRoom = 0.25f;
    [SerializeField]
    private float F_startimeBtwnRoom = 0.25f;
    [SerializeField]
    private float F_minX;
    [SerializeField]
    private float F_minY;
    [SerializeField]
    private float F_maxX;
    [SerializeField]
    private float F_maxY;

    public bool stopGeneration = false;

    public LayerMask room;
    
    public int I_roomcounter;
    public int I_maxRoom;

    public int downcounter;

    public string I_seed;
    [SerializeField]
    GameObject renderMap;
    // Start is called before the first frame update
    void Start()
    {
        I_seed="";
        I_roomcounter = 0;
        I_maxRoom = Random.Range(10,12);
        int randStartPos = Random.Range(0, T_C_startPos.Length);
        transform.position = T_C_startPos[randStartPos].position;
       Instantiate(G_C_A_rooms[0], transform.position, Quaternion.identity);
        I_seed += 0;
        I_roomcounter++;
        I_direction = Random.Range(1, 6);
        I_seed += I_direction;
    }
    private void Update()
    {
        if(F_timeBtwnRoom <= 0 && !stopGeneration)
        {
            V_Move();
            F_timeBtwnRoom = F_startimeBtwnRoom;
        }
        else
        {
            F_timeBtwnRoom -= Time.deltaTime;
        }
    }

    private void V_Move()
    {
        if (I_roomcounter > I_maxRoom)
        {
            stopGeneration = true;
            renderMap.SetActive(true);
        }
        else if (I_direction == 1 || I_direction == 2)//Moving Right
        {
                Vector2 V2_newPos = new Vector2(transform.position.x + F_roomDIfferent, transform.position.y);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
            if (Detection == null)
            {

                transform.position = V2_newPos;

                //Room choosing
                int randroom = Random.Range(0, G_C_A_rooms.Length);
                I_seed += randroom;
                Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);
                
                //direction
                I_direction = Random.Range(1, 6);
                if (I_direction == 3)
                {
                    I_direction = 1;
                }
                else if (I_direction == 4)
                {
                    I_direction = 5;
                }
                downcounter = 0;
                I_roomcounter++;
            }
            else
                I_direction = 5;
        }
        else if (I_direction == 3 || I_direction == 4)//Moving Left
        {
            Vector2 V2_newPos = new Vector2(transform.position.x - F_roomDIfferent, transform.position.y);
            Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
            if (Detection == null)
            {

                transform.position = V2_newPos;
                //room choosing
                int randroom = Random.Range(0, G_C_A_rooms.Length);
                I_seed += randroom;
                Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);
                //direction movement
                I_direction = Random.Range(1, 6);
                if (I_direction == 1)
                {
                    I_direction = 3;
                }
                else if (I_direction == 2)
                {
                    I_direction = 5;
                }
                downcounter = 0;
                I_roomcounter++;
            }
            else
                I_direction = 5;

        }
        else if (I_direction == 5)//Moving Down
        {
                 Vector2 V2_newPos = new Vector2(transform.position.x , transform.position.y - F_roomDIfferent);
            Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
            if (Detection == null)
            {
            ++downcounter;
                Detection = Physics2D.OverlapCircle(transform.position, 1, room);
                if(Detection != null)
                {

                if(Detection.GetComponent<RoomType>().type != 1 )
                {
                    if(Detection.GetComponent<RoomType>().type != 3)
                    {
                        if(downcounter>=2)
                        {
                         Detection.GetComponent<RoomType>().v_DestroyRoom();
                       Instantiate(G_C_A_rooms[3], transform.position, Quaternion.identity);
                          I_seed += 3;
                        }
                        else
                        {

                         Detection.GetComponent<RoomType>().v_DestroyRoom();
                         int randbot = Random.Range(1, 4);
                         if(randbot == 2)
                         {
                                randbot = 1;
                         }
                            I_seed += randbot;
                             Instantiate(G_C_A_rooms[randbot], transform.position, Quaternion.identity);
                        }
                    }
                    
                }
                }

                // Vector2 V2_newPos = new Vector2(transform.position.x , transform.position.y - F_roomDIfferent);
                 transform.position = V2_newPos;


                int randroom = Random.Range(2, G_C_A_rooms.Length);
                I_seed += randroom;
                Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);

                I_direction = Random.Range(1, 6);
                I_roomcounter++;
            }
          
                
        }
        if(I_roomcounter <= I_maxRoom)
        I_seed += I_direction;

    }

}
