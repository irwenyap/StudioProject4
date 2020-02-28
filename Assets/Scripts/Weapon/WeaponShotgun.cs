using Photon.Pun;
using UnityEngine;

public class WeaponShotgun : WeaponBase, IPunObservable {
    public Rigidbody2D projectile;

    private SpriteRenderer weaponSprite;

    void Start() {
        // Disable Photon View on ground to save bandwidth
        photonView.enabled = false;

        // Reference to component
        weaponSprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();

        // Base Stats
        attackDamage = 10;
        attackSpeed = 3f;
        m2Cooldown = 1f;
        cooldown01 = attackSpeed;
        cooldown02 = m2Cooldown;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        throw new System.NotImplementedException();
    }
}
