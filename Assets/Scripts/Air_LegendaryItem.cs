using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air_LegendaryItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<PlayerController>().moveSpeed *=2;        
    }

}
