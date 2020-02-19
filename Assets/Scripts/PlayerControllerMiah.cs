using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMiah : MonoBehaviour
{
    // Public
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    // Private
    Rigidbody2D myRigidbody;
    Vector2 moveAxis;
    Vector3 mousePos;

    int weaponType;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {

        // Movement Input
        moveAxis.x = Input.GetAxisRaw("Horizontal");
        moveAxis.y = Input.GetAxisRaw("Vertical");

        // Cursor Follow
        //mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.z, Camera.main.transform.position.z - transform.position.z));
        //transform.LookAt(mousePos);

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // For healthbar testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }
    }

    private void FixedUpdate() {
        myRigidbody.MovePosition(myRigidbody.position + moveAxis * moveSpeed * Time.deltaTime);
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
