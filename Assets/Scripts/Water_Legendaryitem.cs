using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Legendaryitem : MonoBehaviour
{
    float healTimer = 0;
    public int valueadd = 5;
    // Start is called before the first frame update
    void Start()
    {
                
    }
    private void Update()
    {
        if (healTimer <= 0 && gameObject.GetComponent<PlayerController>().currHealth < gameObject.GetComponent<PlayerController>().maxHealth)
        {
            gameObject.GetComponent<PlayerController>().currHealth += valueadd;
            healTimer = 3.0f;
        }
        else
            healTimer -= Time.deltaTime;
    }



}
