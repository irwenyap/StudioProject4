using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingLevelGen : MonoBehaviour
{
    public LevelGen gen;

    public string seed;
    public int I_direction;
    public int[] A_roomtype;
    public Vector2[] A_roomCord;

    private void Start()
    {
        A_roomtype = gen.L_V2_roomtype.ToArray();
        A_roomCord = gen.L_V2_roomPosition.ToArray();
        for(int i =0; i < gen.L_V2_roomtype.Count;++i)
        {
            Instantiate(gen.G_C_A_rooms[A_roomtype[i]], A_roomCord[i], Quaternion.identity);
        }


    }

}


