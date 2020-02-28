using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponPistol : WeaponBase, IPunObservable {
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

    void Update() {
        if (isAttached) {
            if (!photonView.enabled)
                photonView.enabled = true;
            cooldown01 += Time.deltaTime;
            cooldown02 += Time.deltaTime;

            // Rotation
            if (transform.parent.eulerAngles.z > 0 && transform.parent.eulerAngles.z < 180)
                weaponSprite.flipY = true;
            else
                weaponSprite.flipY = false;

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
                    DropWeapon();
            }

            if (isInUseM1 && cooldown01 >= attackSpeed) {
                Rigidbody2D rb = Instantiate(projectile, transform.position + (transform.right * 0.5f), transform.parent.rotation);
                rb.velocity = rb.gameObject.transform.up * 10;
                cooldown01 = 0f;
            } else if (isInUseM2 && cooldown02 >= m2Cooldown) {
                StartCoroutine("TripleShot");
                cooldown02 = 0f;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(isInUseM1);
            stream.SendNext(isInUseM2);
        }
        else {
            isInUseM1 = (bool)stream.ReceiveNext();
            isInUseM2 = (bool)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.GetComponent<PlayerController>().weaponIsOnHand) {
            myPlayer = collision.GetComponent<PlayerController>();
            myPlayer.weaponIsOnHand = true;
            transform.SetParent(collision.transform.Find("Weapon"));
            WeaponOnHand(true);

            // Setting the item's transform when attached
            transform.localPosition = new Vector3(0, 4, -1);
            transform.localScale = new Vector3(15, 15, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 90);

            // Photon ownership transfer on pickup
            photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        }
    }

    IEnumerator TripleShot() {
        for (int i = 0; i < 3; ++i) {
            Rigidbody2D rb = Instantiate(projectile, transform.position, transform.parent.rotation);
            rb.velocity = rb.gameObject.transform.up * 10;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
