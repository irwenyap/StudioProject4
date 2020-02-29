using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgePage : MonoBehaviour
{
    private WeaponBase currentWeapon;

    private void Start()
    {
        InitPage();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (Input.GetKeyDown(KeyCode.E))
            //currentWeapon = collision.GetComponent<PlayerController>().currentWeapon;
            
    }
    private void InitPage()
    {
        switch (currentWeapon.name)
        {
            case "BOW":
                break;
        }
    }
}
