using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Client Variables
    public string playerName; 

    // Private
    private Rigidbody2D myRigidbody;
    private Vector2 moveAxis;
    private Vector3 mousePos;
    private Animator myAnimator;

    public bool weaponIsOnHand = false;
    public Transform weaponLocation;
    public WeaponBase currentWeapon;

    public Vector2 dir;

    // Stats
    public int maxHealth;
    public int currHealth;
    public float critChance;
    public float critDamage;
    public int armour;
    public float moveSpeed = 5f;
    public float attackSpeed;
    public float attack;
    public int coins;
    public int gems;
    public int shards;

    // Player UI
    private PlayerUI myPlayerUI;


    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        weaponLocation = transform.Find("Weapon");
        myPlayerUI = GetComponent<PlayerUI>();
        //healthBar.SetMaxHealth(maxHealth);

        maxHealth = 100;
        currHealth = maxHealth;
        critChance = 5f;
        critDamage = 10f;
        armour = 2;
        moveSpeed = 5f;
        coins = 50;
    }

    private void Update() {
        // Movement Input
        moveAxis.x = Input.GetAxisRaw("Horizontal");
        moveAxis.y = Input.GetAxisRaw("Vertical");

        // Look at Cursor
        dir = Input.mousePosition - Camera.main.WorldToScreenPoint(weaponLocation.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        weaponLocation.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //weaponLocation.position = transform.position + (1f * dir.normalized);
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (moveAxis != Vector2.zero)
            myAnimator.SetFloat("moveX", moveAxis.x);
    }

    public void TakeDamage(int dmg) {
        if (enabled) {
            int netDamage = dmg - armour;
            if (netDamage <= 0)
                netDamage = 1;

            if (gameObject.TryGetComponent(out Earth_LegendaryItem earth)) {
                if(earth.CurrShieldValue> 0)
                earth.CurrShieldValue -= netDamage;
                else
                currHealth -= netDamage;
            } else { 
                currHealth -= netDamage;
                myPlayerUI.healthBar.healthSystem.Damage(netDamage);
            }
        }
    }

    public void CollectCoins(int _coins) {
        coins += _coins;
        myPlayerUI.coins.text = coins.ToString();
    }

    public void UseCoins(int _coins) {
        coins -= _coins;
        myPlayerUI.coins.text = coins.ToString();
    }

    private void FixedUpdate() {
        myRigidbody.MovePosition(myRigidbody.position + moveAxis * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 11) {
            TakeDamage(collision.gameObject.GetComponent<ProjectileBase>().GetDamage());
        }
    }
}
