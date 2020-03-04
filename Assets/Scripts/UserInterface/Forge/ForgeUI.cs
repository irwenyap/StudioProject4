using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeUI : MonoBehaviour
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
    public Text selectedWeaponName;
    public Image selectedWeapon;
    public Image selectedWeaponM1;
    public Image selectedWeaponM2;

    public Image toolTipBackground;
    public Text skillName;
    public Text skillDescription;

    // <--Upgrades-->


    // Public
    public Sprite bowSprite, laserbowSprite, crossbowSprite, pistolSprite, rifleSprite, shotgunSprite, stickSprite, orbSprite, bookSprite, daggerSprite, swordSprite, scytheSprite;
    public Forge forge;

    // Private
    private WeaponBase currentWeapon;
    private PlayerController playerInfo;
    private int cost;

    private void Start()
    {
        cost = 1;
        InitPage();
    }

    private void InitPage()
    {
        toolTipBackground.enabled = false;
        skillName.enabled = false;
        skillDescription.enabled = false;

        currentWeapon = forge.GetWeaponInfo();
        playerInfo = forge.GetPlayerInfo();

        gemBalance.text = playerInfo.gems.ToString();

        // Skilltree sprites
        switch (currentWeapon.weaponType)
        {
            case WeaponBase.WEAPON_TYPE.BOW:
            case WeaponBase.WEAPON_TYPE.LASERBOW:
            case WeaponBase.WEAPON_TYPE.CROSSBOW:
                {
                    skillTreeBaseWeapon.sprite = bowSprite;
                    skillTreeLeftUpgrade.sprite = laserbowSprite;
                    skillTreeRightUpgrade.sprite = crossbowSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
            case WeaponBase.WEAPON_TYPE.RIFLE:
            case WeaponBase.WEAPON_TYPE.SHOTGUN:
                {
                    skillTreeBaseWeapon.sprite = pistolSprite;
                    skillTreeLeftUpgrade.sprite = rifleSprite;
                    skillTreeRightUpgrade.sprite = shotgunSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
            case WeaponBase.WEAPON_TYPE.ORB:
            case WeaponBase.WEAPON_TYPE.BOOK:
                {
                    skillTreeBaseWeapon.sprite = stickSprite;
                    skillTreeLeftUpgrade.sprite = orbSprite;
                    skillTreeRightUpgrade.sprite = bookSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
            case WeaponBase.WEAPON_TYPE.SWORD:
            case WeaponBase.WEAPON_TYPE.SCYTHE:
                {
                    skillTreeBaseWeapon.sprite = daggerSprite;
                    skillTreeLeftUpgrade.sprite = swordSprite;
                    skillTreeRightUpgrade.sprite = scytheSprite;
                }
                break;
        }

        switch (currentWeapon.weaponType)
        {
            case WeaponBase.WEAPON_TYPE.BOW:
                {
                    currentWeaponImage.sprite = bowSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.LASERBOW:
                {
                    currentWeaponImage.sprite = laserbowSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.CROSSBOW:
                {
                    currentWeaponImage.sprite = crossbowSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
                {
                    currentWeaponImage.sprite = pistolSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.RIFLE:
                {
                    currentWeaponImage.sprite = rifleSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.SHOTGUN:
                {
                    currentWeaponImage.sprite = shotgunSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
                {
                    currentWeaponImage.sprite = stickSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.ORB:
                {
                    currentWeaponImage.sprite = orbSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.BOOK:
                {
                    currentWeaponImage.sprite = bookSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
                {
                    currentWeaponImage.sprite = daggerSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.SWORD:
                {
                    currentWeaponImage.sprite = swordSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.SCYTHE:
                {
                    currentWeaponImage.sprite = scytheSprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.NONE:
                Debug.Log("Fuck yourself");
                break;
        }
    }

    public void ForgeWeapon()
    {
        if (playerInfo.gems >= cost)
        {
            playerInfo.gems -= cost;
        }
    }

    public void SelectLeftWeapon()
    {
        switch (currentWeapon.weaponType)
        {
            case WeaponBase.WEAPON_TYPE.BOW:
            case WeaponBase.WEAPON_TYPE.LASERBOW:
            case WeaponBase.WEAPON_TYPE.CROSSBOW:
                {
                    selectedWeaponName.text = "Laser Bow";
                    selectedWeapon.sprite = laserbowSprite;
                    //selectedWeaponM1.sprite = laserbowM1Sprite;
                    //selectedWeaponM2.sprite = laserbowM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
            case WeaponBase.WEAPON_TYPE.RIFLE:
            case WeaponBase.WEAPON_TYPE.SHOTGUN:
                {
                    selectedWeaponName.text = "Rifle";
                    selectedWeapon.sprite = rifleSprite;
                    //selectedWeaponM1.sprite = rifleM1Sprite;
                    //selectedWeaponM2.sprite = rifleM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
            case WeaponBase.WEAPON_TYPE.ORB:
            case WeaponBase.WEAPON_TYPE.BOOK:
                {
                    selectedWeaponName.text = "Orb";
                    selectedWeapon.sprite = orbSprite;
                    //selectedWeaponM1.sprite = orbM1Sprite;
                    //selectedWeaponM2.sprite = orbM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
            case WeaponBase.WEAPON_TYPE.SWORD:
            case WeaponBase.WEAPON_TYPE.SCYTHE:
                {
                    selectedWeaponName.text = "Sword";
                    selectedWeapon.sprite = swordSprite;
                    //selectedWeaponM1.sprite = swordM1Sprite;
                    //selectedWeaponM2.sprite = swordM2Sprite;
                }
                break;
        }
    }

    public void SelectRightWeapon()
    {
        switch (currentWeapon.weaponType)
        {
            case WeaponBase.WEAPON_TYPE.BOW:
            case WeaponBase.WEAPON_TYPE.LASERBOW:
            case WeaponBase.WEAPON_TYPE.CROSSBOW:
                {
                    selectedWeaponName.text = "Crossbow";
                    selectedWeapon.sprite = crossbowSprite;
                    //selectedWeaponM1.sprite = crossbowM1Sprite;
                    //selectedWeaponM2.sprite = crossbowM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
            case WeaponBase.WEAPON_TYPE.RIFLE:
            case WeaponBase.WEAPON_TYPE.SHOTGUN:
                {
                    selectedWeaponName.text = "Shotgun";
                    selectedWeapon.sprite = shotgunSprite;
                    //selectedWeaponM1.sprite = shotgunM1Sprite;
                    //selectedWeaponM2.sprite = shotgunM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
            case WeaponBase.WEAPON_TYPE.ORB:
            case WeaponBase.WEAPON_TYPE.BOOK:
                {
                    selectedWeaponName.text = "Book";
                    selectedWeapon.sprite = bookSprite;
                    //selectedWeaponM1.sprite = bookM1Sprite;
                    //selectedWeaponM2.sprite = bookM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
            case WeaponBase.WEAPON_TYPE.SWORD:
            case WeaponBase.WEAPON_TYPE.SCYTHE:
                {
                    selectedWeaponName.text = "Scythe";
                    selectedWeapon.sprite = scytheSprite;
                    //selectedWeaponM1.sprite = scytheM1Sprite;
                    //selectedWeaponM2.sprite = scytheM2Sprite;
                }
                break;
        }
    }

    public void SelectBottomWeapon()
    {
        switch (currentWeapon.weaponType)
        {
            case WeaponBase.WEAPON_TYPE.BOW:
            case WeaponBase.WEAPON_TYPE.LASERBOW:
            case WeaponBase.WEAPON_TYPE.CROSSBOW:
                {
                    selectedWeaponName.text = "Bow";
                    selectedWeapon.sprite = bowSprite;
                    //selectedWeaponM1.sprite = bowM1Sprite;
                    //selectedWeaponM2.sprite = bowM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
            case WeaponBase.WEAPON_TYPE.RIFLE:
            case WeaponBase.WEAPON_TYPE.SHOTGUN:
                {
                    selectedWeaponName.text = "Pistol";
                    selectedWeapon.sprite = pistolSprite;
                    //selectedWeaponM1.sprite = pistolM1Sprite;
                    //selectedWeaponM2.sprite = pistolM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
            case WeaponBase.WEAPON_TYPE.ORB:
            case WeaponBase.WEAPON_TYPE.BOOK:
                {
                    selectedWeaponName.text = "Stick";
                    selectedWeapon.sprite = stickSprite;
                    //selectedWeaponM1.sprite = stickM1Sprite;
                    //selectedWeaponM2.sprite = stickM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
            case WeaponBase.WEAPON_TYPE.SWORD:
            case WeaponBase.WEAPON_TYPE.SCYTHE:
                {
                    selectedWeaponName.text = "Dagger";
                    selectedWeapon.sprite = daggerSprite;
                    //selectedWeaponM1.sprite = daggerM1Sprite;
                    //selectedWeaponM2.sprite = daggerM2Sprite;
                }
                break;
        }
    }

    public void M1SkillOnHover()
    {
        switch (selectedWeaponName.text)
        {
            case "Bow":
                {
                    skillName.text = "";
                    skillDescription.text = "";
                }
                break;
            case "Laser Bow":
                {

                }
                break;
            case "Crossbow":
                {

                }
                break;
            case "Pistol":
                {

                }
                break;
            case "Rifle":
                {

                }
                break;
            case "Shotgun":
                {

                }
                break;
            case "Stick":
                {

                }
                break;
            case "Orb":
                {

                }
                break;
            case "Book":
                {

                }
                break;
            case "Dagger":
                {

                }
                break;
            case "Sword":
                {

                }
                break;
            case "Scythe":
                {

                }
                break;
        }

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
