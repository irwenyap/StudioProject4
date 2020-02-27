﻿using System.Collections;
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

    public Vector2 dir;

    // Stats
    public int maxHealth;
    public int currHealth;
    public float critChance;
    public float critDamage;
    public int armour = 2;
    public float moveSpeed = 5f;
    public float attackSpeed;
    public float attack;
    public int currency;
    // UI
    public PlayerHealthBar healthBar;

    public Skills m1;
    public Skills m2;

    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        weaponLocation = transform.Find("Weapon");
		//currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
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

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (weaponIsOnHand) {
                weaponIsOnHand = false;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Q)) {
            //if (weaponOnHand != null) {
        //        //weaponOnHand.Throw(weaponLocation.rotation);
        //        //weaponOnHand.Throw(dir.normalized * 250);
        //        //weaponOnHand.GetWeaponRigidbody().AddForce(dir.normalized * 250);
        //        //weaponOnHand.WeaponOnHand(false);
        //        //weaponOnHand.transform.parent = null;
        //        weaponOnHand = null;
                
            //}
        //}

        if (moveAxis != Vector2.zero)
            myAnimator.SetFloat("moveX", moveAxis.x);
    }

    public void TakeDamage(int dmg) {
        int netDamage = dmg - armour;
        currHealth -= netDamage;
        healthBar.healthSystem.Damage(netDamage);
    }

    private void FixedUpdate() {
        myRigidbody.MovePosition(myRigidbody.position + moveAxis * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 11) {
            TakeDamage(collision.gameObject.GetComponent<ProjectileBase>().damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Weapon") {
            m1.imageSkill.sprite = collision.gameObject.GetComponent<WeaponBase>().imageM1;
            m2.imageSkill.sprite = collision.gameObject.GetComponent<WeaponBase>().imageM2;
        }
    }
}
