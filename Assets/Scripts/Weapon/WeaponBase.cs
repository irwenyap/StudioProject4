using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponBase : MonoBehaviourPun {
    protected int WeaponID;
    public bool isAttached = false;

    protected BoxCollider2D myCollider;
    protected Rigidbody2D myRigidbody;

    public Rigidbody2D GetWeaponRigidbody() { return myRigidbody; }
    public Collider2D GetWeaponCollider() { return myCollider; }

    public void WeaponOnHand(bool status) {
        if (status) {
            isAttached = true;
            myRigidbody.isKinematic = true;
            myCollider.enabled = false;
        } else {
            isAttached = false;
            myRigidbody.isKinematic = false;
        }
    }

    public void Throw(Vector2 force) {
        StartCoroutine("ThrowCoroutine");
        transform.parent = null;
        WeaponOnHand(false);
        myRigidbody.AddForce(force);
    }

    IEnumerator ThrowCoroutine() {
        while (!myCollider.enabled) {
            if (myRigidbody.velocity == Vector2.zero && !isAttached) {
                myCollider.enabled = true;
            }
            yield return null;
        }
    }
}
