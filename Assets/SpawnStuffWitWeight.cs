using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStuffWitWeight : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Objects;
    private int[] weights = {
        44,
        20,
        15,
        15,
        5,
        1
        };//this need to crospond with the objects array
    // Start is called before the first frame update
    public int random;
    void Start()
    {
        int rand = Random.Range(0, 100);
        random = rand;
       for (int i = 0; i < weights.Length;++i)
       {
         if(random < weights[i])
         {
                Instantiate(Objects[i], transform.position, Quaternion.identity);
                break;
         }
            random -= weights[i]; 

       }
        
    }
}
