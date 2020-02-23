using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Client Variables
    public uint id;
    public string playerName;
	
    public float moveSpeed = 5f;    

    // Private
    private Rigidbody2D myRigidbody;
    private Vector2 moveAxis;
    private Vector3 mousePos;
    bool isPlayer = false;
    private Animator myAnimator;


    public WeaponBase weaponOnHand;
    public Transform weaponLocation;

    int weaponType;
	
    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        weaponLocation = transform.Find("Weapon");
		//currentHealth = maxHealth;
  //      healthBar.SetMaxHealth(maxHealth);
    }

    public void Initialise(bool _isPlayer) {
        isPlayer = _isPlayer;
    }

    private void Update() {
        //if (isPlayer) {
            // Movement Input
        moveAxis.x = Input.GetAxisRaw("Horizontal");
        moveAxis.y = Input.GetAxisRaw("Vertical");

        // Look at Cursor
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(weaponLocation.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        weaponLocation.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //weaponLocation.position = transform.position + (1f * dir.normalized);
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (weaponOnHand != null) {
                weaponOnHand.Throw(dir.normalized * 250);
                //weaponOnHand.GetWeaponRigidbody().AddForce(dir.normalized * 250);
                //weaponOnHand.WeaponOnHand(false);
                //weaponOnHand.transform.parent = null;
                weaponOnHand = null;
            }
        }

        // For healthbar testing
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    TakeDamage(5);
        //}

        if (moveAxis != Vector2.zero)
            myAnimator.SetFloat("moveX", moveAxis.x);
    }

    private void FixedUpdate() {
        myRigidbody.MovePosition(myRigidbody.position + moveAxis * moveSpeed * Time.deltaTime);
    }
}
