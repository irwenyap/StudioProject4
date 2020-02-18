using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingLevelGen : MonoBehaviour
{
    public LevelGen gen;

    public string seed;
    public int I_direction;

    private void Start()
    {
        seed = gen.I_seed;
        Instantiate(gen.G_C_A_rooms[int.Parse(seed[0].ToString())], transform.position, Quaternion.identity);
        I_direction = int.Parse(seed[1].ToString());
        
        for (int i = 2; i<seed.Length;++i)
        {
            if (I_direction == 1 || I_direction == 2)//Moving Right
            {
                Vector2 V2_newPos = new Vector2(transform.position.x + gen.F_roomDIfferent, transform.position.y);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, gen.room);
                if (Detection == null)
                {

                    transform.position = V2_newPos;

                    Instantiate(gen.G_C_A_rooms[int.Parse(seed[i++].ToString())], transform.position, Quaternion.identity);

                }
             
            }
            else if (I_direction == 3 || I_direction == 4)//Moving Left
            {
                Vector2 V2_newPos = new Vector2(transform.position.x - gen.F_roomDIfferent, transform.position.y);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, gen.room);
                if (Detection == null)
                {

                    transform.position = V2_newPos;
                    //room choosing
                    
                    Instantiate(gen.G_C_A_rooms[int.Parse(seed[i++].ToString())], transform.position, Quaternion.identity);
                   
                }
             

            }
            else if (I_direction == 5)//Moving Down
            {
               
                Vector2 V2_newPos = new Vector2(transform.position.x, transform.position.y - gen.F_roomDIfferent);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, gen.room);
                if (Detection == null)
                {
                    Detection = Physics2D.OverlapCircle(transform.position, 1, gen.room);
                    if (Detection.GetComponent<RoomType>().type != 1)
                    {
                        if (Detection.GetComponent<RoomType>().type != 3)
                        {
                           
                          

                                Detection.GetComponent<RoomType>().v_DestroyRoom();
                        
                                Instantiate(gen.G_C_A_rooms[int.Parse(seed[i++].ToString())], transform.position, Quaternion.identity);
                          
                        }

                    }

                    // Vector2 V2_newPos = new Vector2(transform.position.x , transform.position.y - F_roomDIfferent);
                    transform.position = V2_newPos;

                    Instantiate(gen.G_C_A_rooms[int.Parse(seed[i++].ToString())], transform.position, Quaternion.identity);

                }


            }
            if(i < seed.Length)
            I_direction = int.Parse(seed[i].ToString());
        }
    }
}


