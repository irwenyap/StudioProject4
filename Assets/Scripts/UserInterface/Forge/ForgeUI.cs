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

    // Public
    public Sprite bowSprite, laserbowSprite, crossbowSprite, pistolSprite, rifleSprite, shotgunSprite, stickSprite, orbSprite, bookSprite, daggerSprite, swordSprite, scytheSprite;
    public Sprite bowM1Sprite, bowM2Sprite, laserbowM1Sprite, laserbowM2Sprite, crossbowM1Sprite, crossbowM2Sprite, pistolM1Sprite, pistolM2Sprite, rifleM1Sprite, rifleM2Sprite, shotgunM1Sprite, shotgunM2Sprite,
                  stickM1Sprite, stickM2Sprite, orbM1Sprite, orbM2Sprite, bookM1Sprite, bookM2Sprite, daggerM1Sprite, daggerM2Sprite, swordM1Sprite, swordM2Sprite, scytheM1Sprite, scytheM2Sprite;
    public GameObject laserbowObject, crossbowObject, rifleObject, shotgunObject, orbObject, bookObject, swordObject, scytheObject;
    public Forge forge;
    public GameObject gameplayUI;
    public GameObject forgeUI;

    // Private
    private WeaponBase currentWeapon;
    private PlayerController playerInfo;
    private int cost;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            forgeUI.SetActive(false);
            gameplayUI.SetActive(true);
        }
    }

    public void InitPage()
    {
        cost = 1;
        toolTipBackground.enabled = false;
        skillName.enabled = false;
        skillDescription.enabled = false;

        currentWeapon = forge.GetWeaponInfo();
        playerInfo = forge.GetPlayerInfo();

        gemBalance.text = playerInfo.gems.ToString();
        forgeCost.text = cost.ToString();

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

                    selectedWeaponName.text = "Bow";
                    selectedWeapon.sprite = bowSprite;
                    selectedWeaponM1.sprite = bowM1Sprite;
                    selectedWeaponM2.sprite = bowM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.LASERBOW:
                {
                    currentWeaponImage.sprite = laserbowSprite;

                    selectedWeaponName.text = "Laser Bow";
                    selectedWeapon.sprite = laserbowSprite;
                    selectedWeaponM1.sprite = laserbowM1Sprite;
                    selectedWeaponM2.sprite = laserbowM2Sprite;

                    lockImageLeft.enabled = false;
                }
                break;
            case WeaponBase.WEAPON_TYPE.CROSSBOW:
                {
                    currentWeaponImage.sprite = crossbowSprite;

                    selectedWeaponName.text = "Crossbow";
                    selectedWeapon.sprite = crossbowSprite;
                    selectedWeaponM1.sprite = crossbowM1Sprite;
                    selectedWeaponM2.sprite = crossbowM2Sprite;

                    lockImageRight.enabled = false;
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
                {
                    currentWeaponImage.sprite = pistolSprite;

                    selectedWeaponName.text = "Pistol";
                    selectedWeapon.sprite = pistolSprite;
                    selectedWeaponM1.sprite = pistolM1Sprite;
                    selectedWeaponM2.sprite = pistolM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.RIFLE:
                {
                    currentWeaponImage.sprite = rifleSprite;

                    selectedWeaponName.text = "Rifle";
                    selectedWeapon.sprite = rifleSprite;
                    selectedWeaponM1.sprite = rifleM1Sprite;
                    selectedWeaponM2.sprite = rifleM2Sprite;

                    lockImageLeft.enabled = false;
                }
                break;
            case WeaponBase.WEAPON_TYPE.SHOTGUN:
                {
                    currentWeaponImage.sprite = shotgunSprite;

                    selectedWeaponName.text = "Shotgun";
                    selectedWeapon.sprite = shotgunSprite;
                    selectedWeaponM1.sprite = shotgunM1Sprite;
                    selectedWeaponM2.sprite = shotgunM2Sprite;

                    lockImageRight.enabled = false;
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
                {
                    currentWeaponImage.sprite = stickSprite;

                    selectedWeaponName.text = "Stick";
                    selectedWeapon.sprite = stickSprite;
                    selectedWeaponM1.sprite = stickM1Sprite;
                    selectedWeaponM2.sprite = stickM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.ORB:
                {
                    currentWeaponImage.sprite = orbSprite;

                    selectedWeaponName.text = "Orb";
                    selectedWeapon.sprite = orbSprite;
                    selectedWeaponM1.sprite = orbM1Sprite;
                    selectedWeaponM2.sprite = orbM2Sprite;

                    lockImageLeft.enabled = false;
                }
                break;
            case WeaponBase.WEAPON_TYPE.BOOK:
                {
                    currentWeaponImage.sprite = bookSprite;

                    selectedWeaponName.text = "Book";
                    selectedWeapon.sprite = bookSprite;
                    selectedWeaponM1.sprite = bookM1Sprite;
                    selectedWeaponM2.sprite = bookM2Sprite;

                    lockImageRight.enabled = false;
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
                {
                    currentWeaponImage.sprite = daggerSprite;

                    selectedWeaponName.text = "Dagger";
                    selectedWeapon.sprite = daggerSprite;
                    selectedWeaponM1.sprite = daggerM1Sprite;
                    selectedWeaponM2.sprite = daggerM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.SWORD:
                {
                    currentWeaponImage.sprite = swordSprite;

                    selectedWeaponName.text = "Sword";
                    selectedWeapon.sprite = swordSprite;
                    selectedWeaponM1.sprite = swordM1Sprite;
                    selectedWeaponM2.sprite = swordM2Sprite;

                    lockImageLeft.enabled = false;
                }
                break;
            case WeaponBase.WEAPON_TYPE.SCYTHE:
                {
                    currentWeaponImage.sprite = scytheSprite;

                    selectedWeaponName.text = "Scythe";
                    selectedWeapon.sprite = scytheSprite;
                    selectedWeaponM1.sprite = scytheM1Sprite;
                    selectedWeaponM2.sprite = scytheM2Sprite;

                    lockImageRight.enabled = false;
                }
                break;
            case WeaponBase.WEAPON_TYPE.NONE:
                break;
        }
    }

    public void ForgeWeapon()
    {
        switch (currentWeapon.weaponType)
        {
            case WeaponBase.WEAPON_TYPE.BOW:
                {
                    switch (selectedWeaponName.text)
                    {
                        case "Laser Bow":
                            {
                                if (playerInfo.gems >= cost)
                                {
                                    playerInfo.gems -= cost;
                                    Destroy(currentWeapon.gameObject);
                                    playerInfo.weaponIsOnHand = false;
                                    GameObject instance = (GameObject)Instantiate(laserbowObject, playerInfo.transform.position, Quaternion.identity);
                                    forgeUI.SetActive(false);
                                    gameplayUI.SetActive(true);
                                }
                            }
                            break;
                        case "Crossbow":
                            {
                                if (playerInfo.gems >= cost)
                                {
                                    playerInfo.gems -= cost;
                                    Destroy(currentWeapon.gameObject);
                                    playerInfo.weaponIsOnHand = false;
                                    GameObject instance = (GameObject)Instantiate(crossbowObject, playerInfo.transform.position, Quaternion.identity);
                                    forgeUI.SetActive(false);
                                    gameplayUI.SetActive(true);
                                }
                            }
                            break;
                    }
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
                {
                    switch (selectedWeaponName.text)
                    {
                        case "Rifle":
                            {
                                if (playerInfo.gems >= cost)
                                {
                                    playerInfo.gems -= cost;
                                    Destroy(currentWeapon.gameObject);
                                    playerInfo.weaponIsOnHand = false;
                                    GameObject instance = (GameObject)Instantiate(rifleObject, playerInfo.transform.position, Quaternion.identity);
                                    forgeUI.SetActive(false);
                                    gameplayUI.SetActive(true);
                                }
                            }
                            break;
                        case "Shotgun":
                            {
                                if (playerInfo.gems >= cost)
                                {
                                    playerInfo.gems -= cost;
                                    Destroy(currentWeapon.gameObject);
                                    playerInfo.weaponIsOnHand = false;
                                    GameObject instance = (GameObject)Instantiate(shotgunObject, playerInfo.transform.position, Quaternion.identity);
                                    forgeUI.SetActive(false);
                                    gameplayUI.SetActive(true);
                                }
                            }
                            break;
                    }
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
                {
                    switch (selectedWeaponName.text)
                    {
                        case "Orb":
                            {
                                if (playerInfo.gems >= cost)
                                {
                                    playerInfo.gems -= cost;
                                    Destroy(currentWeapon.gameObject);
                                    playerInfo.weaponIsOnHand = false;
                                    GameObject instance = (GameObject)Instantiate(orbObject, playerInfo.transform.position, Quaternion.identity);
                                    forgeUI.SetActive(false);
                                    gameplayUI.SetActive(true);
                                }
                            }
                            break;
                        case "Book":
                            {
                                if (playerInfo.gems >= cost)
                                {
                                    playerInfo.gems -= cost;
                                    Destroy(currentWeapon.gameObject);
                                    playerInfo.weaponIsOnHand = false;
                                    GameObject instance = (GameObject)Instantiate(bookObject, playerInfo.transform.position, Quaternion.identity);
                                    forgeUI.SetActive(false);
                                    gameplayUI.SetActive(true);
                                }
                            }
                            break;
                    }
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
                {
                    switch (selectedWeaponName.text)
                    {
                        case "Sword":
                            {
                                playerInfo.gems -= cost;
                                Destroy(currentWeapon.gameObject);
                                playerInfo.weaponIsOnHand = false;
                                GameObject instance = (GameObject)Instantiate(swordObject, playerInfo.transform.position, Quaternion.identity);
                                forgeUI.SetActive(false);
                                gameplayUI.SetActive(true);
                            }
                            break;
                        case "Scythe":
                            {
                                playerInfo.gems -= cost;
                                Destroy(currentWeapon.gameObject);
                                playerInfo.weaponIsOnHand = false;
                                GameObject instance = (GameObject)Instantiate(scytheObject, playerInfo.transform.position, Quaternion.identity);
                                forgeUI.SetActive(false);
                                gameplayUI.SetActive(true);
                            }
                            break;
                    }
                }
                break;
            default:
                break;
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
                    selectedWeaponM1.sprite = laserbowM1Sprite;
                    selectedWeaponM2.sprite = laserbowM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
            case WeaponBase.WEAPON_TYPE.RIFLE:
            case WeaponBase.WEAPON_TYPE.SHOTGUN:
                {
                    selectedWeaponName.text = "Rifle";
                    selectedWeapon.sprite = rifleSprite;
                    selectedWeaponM1.sprite = rifleM1Sprite;
                    selectedWeaponM2.sprite = rifleM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
            case WeaponBase.WEAPON_TYPE.ORB:
            case WeaponBase.WEAPON_TYPE.BOOK:
                {
                    selectedWeaponName.text = "Orb";
                    selectedWeapon.sprite = orbSprite;
                    selectedWeaponM1.sprite = orbM1Sprite;
                    selectedWeaponM2.sprite = orbM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
            case WeaponBase.WEAPON_TYPE.SWORD:
            case WeaponBase.WEAPON_TYPE.SCYTHE:
                {
                    selectedWeaponName.text = "Sword";
                    selectedWeapon.sprite = swordSprite;
                    selectedWeaponM1.sprite = swordM1Sprite;
                    selectedWeaponM2.sprite = swordM2Sprite;
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
                    selectedWeaponM1.sprite = crossbowM1Sprite;
                    selectedWeaponM2.sprite = crossbowM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
            case WeaponBase.WEAPON_TYPE.RIFLE:
            case WeaponBase.WEAPON_TYPE.SHOTGUN:
                {
                    selectedWeaponName.text = "Shotgun";
                    selectedWeapon.sprite = shotgunSprite;
                    selectedWeaponM1.sprite = shotgunM1Sprite;
                    selectedWeaponM2.sprite = shotgunM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
            case WeaponBase.WEAPON_TYPE.ORB:
            case WeaponBase.WEAPON_TYPE.BOOK:
                {
                    selectedWeaponName.text = "Book";
                    selectedWeapon.sprite = bookSprite;
                    selectedWeaponM1.sprite = bookM1Sprite;
                    selectedWeaponM2.sprite = bookM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
            case WeaponBase.WEAPON_TYPE.SWORD:
            case WeaponBase.WEAPON_TYPE.SCYTHE:
                {
                    selectedWeaponName.text = "Scythe";
                    selectedWeapon.sprite = scytheSprite;
                    selectedWeaponM1.sprite = scytheM1Sprite;
                    selectedWeaponM2.sprite = scytheM2Sprite;
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
                    selectedWeaponM1.sprite = bowM1Sprite;
                    selectedWeaponM2.sprite = bowM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.PISTOL:
            case WeaponBase.WEAPON_TYPE.RIFLE:
            case WeaponBase.WEAPON_TYPE.SHOTGUN:
                {
                    selectedWeaponName.text = "Pistol";
                    selectedWeapon.sprite = pistolSprite;
                    selectedWeaponM1.sprite = pistolM1Sprite;
                    selectedWeaponM2.sprite = pistolM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.STICK:
            case WeaponBase.WEAPON_TYPE.ORB:
            case WeaponBase.WEAPON_TYPE.BOOK:
                {
                    selectedWeaponName.text = "Stick";
                    selectedWeapon.sprite = stickSprite;
                    selectedWeaponM1.sprite = stickM1Sprite;
                    selectedWeaponM2.sprite = stickM2Sprite;
                }
                break;
            case WeaponBase.WEAPON_TYPE.DAGGER:
            case WeaponBase.WEAPON_TYPE.SWORD:
            case WeaponBase.WEAPON_TYPE.SCYTHE:
                {
                    selectedWeaponName.text = "Dagger";
                    selectedWeapon.sprite = daggerSprite;
                    selectedWeaponM1.sprite = daggerM1Sprite;
                    selectedWeaponM2.sprite = daggerM2Sprite;
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
                    skillName.text = "Shoot";
                    skillDescription.text = "Fire an arrow";
                }
                break;
            case "Laser Bow":
                {
                    skillName.text = "Piercing Shot";
                    skillDescription.text = "Fire a piercing arrow";
                }
                break;
            case "Crossbow":
                {
                    skillName.text = "Sharp Shot";
                    skillDescription.text = "Fire a sharp arrow";
                }
                break;
            case "Pistol":
                {
                    skillName.text = "Shoot";
                    skillDescription.text = "Fire a bullet";
                }
                break;
            case "Rifle":
                {
                    skillName.text = "Rapid Fire";
                    skillDescription.text = "Fire a bullet at a quicker rate";
                }
                break;
            case "Shotgun":
                {
                    skillName.text = "Blast";
                    skillDescription.text = "Fire a cone of bullets";
                }
                break;
            case "Stick":
                {
                    skillName.text = "Magic Orb";
                    skillDescription.text = "Cast an orb at your enemy";
                }
                break;
            case "Orb":
                {
                    skillName.text = "Enchanted Orb";
                    skillDescription.text = "Cast a powerful orb at your enemy";
                }
                break;
            case "Book":
                {
                    skillName.text = "Enchanted Orb";
                    skillDescription.text = "Cast a powerful orb at your enemy";
                }
                break;
            case "Dagger":
                {
                    skillName.text = "";
                    skillDescription.text = "";
                }
                break;
            case "Sword":
                {
                    skillName.text = "";
                    skillDescription.text = "";
                }
                break;
            case "Scythe":
                {
                    skillName.text = "";
                    skillDescription.text = "";
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
        switch (selectedWeaponName.text)
        {
            case "Bow":
                {
                    skillName.text = "Volley";
                    skillDescription.text = "Fire a cone of arrows";
                }
                break;
            case "Laser Bow":
                {
                    skillName.text = "Charged Shot";
                    skillDescription.text = "Charge an pierce through enemies";
                }
                break;
            case "Crossbow":
                {
                    skillName.text = "Snipe";
                    skillDescription.text = "Fire a powerful shot at your enemy";
                }
                break;
            case "Pistol":
                {
                    skillName.text = "Rapid Fire";
                    skillDescription.text = "Fire three bullets rapidly";
                }
                break;
            case "Rifle":
                {
                    skillName.text = "Missile";
                    skillDescription.text = "Fire a missile that tracks the closest enemy";
                }
                break;
            case "Shotgun":
                {
                    skillName.text = "Double Shot";
                    skillDescription.text = "Fires a cone of bullets twice";
                }
                break;
            case "Stick":
                {
                    skillName.text = "";
                    skillDescription.text = "";
                }
                break;
            case "Orb":
                {
                    skillName.text = "Blackhole";
                    skillDescription.text = "Fire a blackhole that sucks enemies to it";
                }
                break;
            case "Book":
                {
                    skillName.text = "Minion";
                    skillDescription.text = "Summon a friendly minion";
                }
                break;
            case "Dagger":
                {
                    skillName.text = "";
                    skillDescription.text = "";
                }
                break;
            case "Sword":
                {
                    skillName.text = "";
                    skillDescription.text = "";
                }
                break;
            case "Scythe":
                {
                    skillName.text = "";
                    skillDescription.text = "";
                }
                break;
        }

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
