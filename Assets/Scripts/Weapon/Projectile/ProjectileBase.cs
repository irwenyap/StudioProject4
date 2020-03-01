using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {
    protected int damage = 5;

    public void SetDamage(int _damage) { damage = _damage; }
    public int GetDamage() { return damage; }
}
