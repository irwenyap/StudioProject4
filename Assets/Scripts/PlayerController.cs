using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Client Variables
    public uint id;
    public string playerName;
    public bool useWeapon = false;
	
    public float moveSpeed = 5f;    
	public int maxHealth = 100;
    public int currentHealth;

	public HealthBar healthBar;

    // Private
    Rigidbody2D myRigidbody;
    Vector2 moveAxis;
    Vector3 mousePos;
    bool isPlayer = false;

    int weaponType;
	
    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Input.GetMouseButtonDown(0)) {
                useWeapon = true;
            } else if (Input.GetMouseButtonUp(0)) {
                useWeapon = false;
            }
			
			// For healthbar testing
			if (Input.GetKeyDown(KeyCode.Space)) {
				TakeDamage(5);
			}

       //}
    }

    private void FixedUpdate() {
        myRigidbody.MovePosition(myRigidbody.position + moveAxis * moveSpeed * Time.deltaTime);
    }
	
	private void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
