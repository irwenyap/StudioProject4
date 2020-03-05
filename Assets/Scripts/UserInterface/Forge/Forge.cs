using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour
{
    public GameObject gameplayUI;
    public GameObject forgeUI;

    private WeaponBase currentWeapon;
    private PlayerController playerInfo;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentWeapon = collision.GetComponent<PlayerController>().currentWeapon;
                playerInfo = collision.GetComponent<PlayerController>();
                OpenForge();
            }
        }
    }

    private void OpenForge()
    {
        gameplayUI.SetActive(false);
        forgeUI.SetActive(true);
        forgeUI.GetComponent<ForgeUI>().InitPage();
    }

    public WeaponBase GetWeaponInfo()
    {
        return currentWeapon;
    }

    public PlayerController GetPlayerInfo()
    {
        return playerInfo;
    }
}
