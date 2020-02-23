using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponBase : MonoBehaviourPun {
    protected int WeaponID;
    public bool isAttached = false;

    protected BoxCollider2D myCollider;
    protected Rigidbody2D myRigidbody;

    public void WeaponOnHand(bool status) {
        if (status) {
            isAttached = true;
            myRigidbody.isKinematic = true;
            myCollider.enabled = false;
        } else {
            isAttached = false;
            myRigidbody.isKinematic = false;
            StartCoroutine("Throw");
        }
    }

    IEnumerator Throw() {
        if (myRigidbody.velocity == Vector2.zero) {
            myCollider.enabled = true;
            yield return null;
        }
    }
    //public void DropWeapon() {

    //}

    //public void PickUpWeapon() {

    //}
}
