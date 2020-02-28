using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forge : MonoBehaviour
{
    // <--Forge-->
    // Right
    public Image currentWeaponImage;
    public Text forgeCost;
    public Text gemBalance;

    // Middle
    public Image skillTreeBaseWeapon;
    public Image skillTreeLeftUpgrade;
    public Image lockImageLeft;
    public Image skillTreeRightUpgrade;
    public Image lockImageRight;

    // Left
    public Image selectedWeapon;
    public Image selectedWeaponM1;
    public Image selectedWeaponM2;

    public Image toolTipBackground;
    public Text skillName;
    public Text skillDescription;

    // <--Upgrades-->


    public WeaponBase currentWeapon;
    public PlayerController playerInfo;
    public Sprite bowSprite, pistolSprite, stickSprite, daggerSprite;

    private int cost;

    private void Start()
    {
        cost = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InitPage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentWeapon = collision.GetComponent<PlayerController>().currentWeapon;
            playerInfo = collision.GetComponent<PlayerController>();
            InitPage();
        }
    }

    private void InitPage()
    {
        //gemBalance.text = playerInfo.gemBalance;

        switch (currentWeapon.weaponType)
        {
            case WeaponBase.WEAPON_TYPE.BOW:
            //case WeaponBase.WEAPON_TYPE.LASER_BOW:
            //case WeaponBase.WEAPON_TYPE.CROSSBOW:
                {
                    currentWeaponImage.sprite = bowSprite;
                    skillTreeBaseWeapon.sprite = bowSprite;
                    //skillTreeLeftUpgrade.sprite = laserBowSprite;
                    //skillTreeRightUpgrade.sprite = crossbowSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
            //case WeaponBase.WEAPON_TYPE.RIFLE:
            //case WeaponBase.WEAPON_TYPE.SHOTGUN:
                {
                    currentWeaponImage.sprite = pistolSprite;
                    skillTreeBaseWeapon.sprite = pistolSprite;
                    //skillTreeLeftUpgrade.sprite = rifleSprite;
                    //skillTreeRightUpgrade.sprite = shotgunSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
            //case WeaponBase.WEAPON_TYPE.ORB:
            //case WeaponBase.WEAPON_TYPE.BOOK:
                {
                    currentWeaponImage.sprite = stickSprite;
                    skillTreeBaseWeapon.sprite = stickSprite;
                    //skillTreeLeftUpgrade.sprite = orbSprite;
                    //skillTreeRightUpgrade.sprite = bookSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
            //case WeaponBase.WEAPON_TYPE.SWORD:
            //case WeaponBase.WEAPON_TYPE.AXE:
                {
                    currentWeaponImage.sprite = daggerSprite;
                    skillTreeBaseWeapon.sprite = daggerSprite;
                    //skillTreeLeftUpgrade.sprite = swordSprite;
                    //skillTreeRightUpgrade.sprite = axeSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.NONE:
                Debug.Log("Fuck yourself");
                break;
        }
    }

    public void ForgeWeapon()
    {
        //if (playerInfo.gemBalance >= cost)
        //{
        //    playerInfo.gemBalance -= cost;
        //}
    }

    public void M1SkillOnHover()
    {
        toolTipBackground.enabled = true;
        skillName.enabled = true;
        skillDescription.enabled = true;
    }

    public void M1SkillOffHover()
    {
        toolTipBackground.enabled = false;
        skillName.enabled = false;
        skillDescription.enabled = false;
    }

    public void M2SkillOnHover()
    {
        toolTipBackground.enabled = true;
        skillName.enabled = true;
        skillDescription.enabled = true;
    }

    public void M2SkillOffHover()
    {
        toolTipBackground.enabled = false;
        skillName.enabled = false;
        skillDescription.enabled = false;
    }
}
