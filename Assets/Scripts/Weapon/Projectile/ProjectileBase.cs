using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {
    protected int damage = 5;
    protected Rigidbody2D myRigidbody;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void ShootProjectile() {
        myRigidbody.velocity = transform.up * 10;
    }

    public void SetDamage(int _damage) { damage = _damage; }
    public int GetDamage() { return damage; }
}
