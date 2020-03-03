using Photon.Pun;
using UnityEngine;

public class WeaponDagger : WeaponBase {
    public Rigidbody2D projectile;

    private SpriteRenderer weaponSprite;
    private Transform location;

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

            if (isInUseM1) {
                var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                var towards = transform.position + (5 * dir.normalized);
                transform.position = Vector3.MoveTowards(transform.position, towards, Time.deltaTime);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.GetComponent<PlayerController>().weaponIsOnHand) {
            myPlayer = collision.GetComponent<PlayerController>();
            myPlayer.weaponIsOnHand = true;
            transform.SetParent(collision.transform);
            WeaponOnHand(true);

            // Setting the item's transform when attached
            transform.localPosition = new Vector3(0.2f, -0.15f, -1);
            transform.localScale = new Vector3(5, 5, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 45);

            // Photon ownership transfer on pickup
            photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        }
    }
}
