using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_LegendaryItem : MonoBehaviour
{
    int ShieldValueMax;
    public int CurrShieldValue;
    float regentime=15.0f;

    private void Start()
    {
        ShieldValueMax = CurrShieldValue = 300;
    }
    private void Update()
    {
        if(CurrShieldValue <= 0)
        {
            regentime -= Time.deltaTime;
        }
        if(regentime <= 0 )
        {
            CurrShieldValue = ShieldValueMax;
            regentime = 15.0f;
        }
    }

}
