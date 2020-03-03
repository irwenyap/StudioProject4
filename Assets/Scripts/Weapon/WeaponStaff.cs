using Photon.Pun;
using UnityEngine;

public class WeaponStaff : WeaponBase {
    public Rigidbody2D projectile;

    private SpriteRenderer weaponSprite;
    private Transform projDir;

    void Start() {
        // Reference to component
        weaponSprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();

        // Base Stats
        attackDamage = 10;
        attackSpeed = 0.3f;
        m2Cooldown = 1f;
        cooldown01 = attackSpeed;
        cooldown02 = m2Cooldown;
    }

    void Update() {
        if (isAttached) {
            cooldown01 += Time.deltaTime;
            cooldown02 += Time.deltaTime;

            // Direction

            // Shooting
            if (photonView.IsMine) {
                if (Input.GetMouseButton(0))
                    isInUseM1 = true;
                else if (!Input.GetMouseButton(0))
                    isInUseM1 = false;

                if (Input.GetMouseButton(1))
                    isInUseM2 = true;
                else if (!Input.GetMouseButton(1))
                    isInUseM2 = false;

                if (Input.GetKeyDown(KeyCode.Q))
                    photonView.RPC("RPC_ThrowWeapon", RpcTarget.All, GetComponentInParent<PlayerController>().dir.normalized);
            }

            if (isInUseM1 && cooldown01 >= attackSpeed) {
                photonView.RPC("RPC_ShootMouseOne", RpcTarget.All);
                cooldown01 = 0f;
            }
            else if (isInUseM2 && cooldown02 >= m2Cooldown) {
                photonView.RPC("RPC_ShootMouseTwo", RpcTarget.All);
                cooldown02 = 0f;
            }
        }
    }

    public override void ShootMouseOne() {
        Rigidbody2D rb = Instantiate(projectile, transform.position, projDir.rotation);
        rb.velocity = rb.gameObject.transform.up * 10;
    }

    public override void ShootMouseTwo() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.GetComponent<PlayerController>().weaponIsOnHand) {
            myPlayer = collision.GetComponent<PlayerController>();
            myPlayer.weaponIsOnHand = true;
            projDir = transform.parent.Find("Weapon");
            WeaponOnHand(true);

            // Setting the item's transform when attached
            transform.localPosition = new Vector3(-0.5f, 0.1f, -1);
            transform.localScale = new Vector3(2.5f, 2.5f, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 45);

            // Photon ownership transfer on pickup
            photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        }
    }
}
