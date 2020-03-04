using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySystem : MonoBehaviour
{
    [SerializeField]
   static public int difficulty;
    [SerializeField]
   static public int roomentered;
    [SerializeField]
    static public int MaxWeight;
    private void Awake()
    {
        difficulty = 1;
        roomentered = 0;
        MaxWeight = 4;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(roomentered>=10)
        {
            MaxWeight += 20;
            difficulty++;
            roomentered = 0;
        }
        Debug.LogError("MaxWeight: " + MaxWeight);
        Debug.LogError("Difficulty: " + difficulty);
        Debug.LogError("RoomEntered: " + roomentered);
    }
}
