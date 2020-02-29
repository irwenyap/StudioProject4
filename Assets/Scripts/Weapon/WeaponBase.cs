using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBase : MonoBehaviourPun {

    public enum WEAPON_TYPE {
        BOW,
        PISTOL,
        STICK,
        DAGGER
    }

    public bool isAttached = false;

    protected bool isInUseM1 = false;
    protected bool isInUseM2 = false;
    //protected bool isThrown = false;
    protected BoxCollider2D myCollider;
    protected Rigidbody2D myRigidbody;
    [SerializeField]
    protected PlayerController myPlayer;

    public WEAPON_TYPE weaponType;

    // Stats
    public int attackDamage = 0;
    public float attackSpeed = 0f;
    protected float cooldown01 = 0f;
    protected float cooldown02 = 0f;
    protected float m2Cooldown = 0f;

    // UI
    public Sprite imageM1;
    public Sprite imageM2;

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
        WeaponOnHand(false);
        myRigidbody.AddForce(force);
        transform.parent = null;
    }

    IEnumerator ThrowCoroutine() {
        while (!myCollider.enabled) {
            if (myRigidbody.velocity == Vector2.zero && transform.parent == null) {
                myCollider.enabled = true;
                //photonView.TransferOwnership(0);
            }
            yield return null;
        }
    }

    [PunRPC]
    protected void RPC_ThrowWeapon(Vector2 dir) {
        Debug.LogError("THROWING DETECTED");
        myPlayer.weaponIsOnHand = false;
        Throw(dir * 250);
    }

    protected void DropWeapon() {
        base.photonView.RPC("RPC_ThrowWeapon", RpcTarget.All, GetComponentInParent<PlayerController>().dir.normalized);
    }
}
