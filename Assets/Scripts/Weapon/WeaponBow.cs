using Photon.Pun;
using UnityEngine;

public class WeaponBow : WeaponBase, IPunObservable {
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

        // Weapon Type
        weaponType = WEAPON_TYPE.BOW;
    }

    void Update() {
        if (isAttached) {
            cooldown01 += Time.deltaTime;
            cooldown02 += Time.deltaTime;

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
                Rigidbody2D rb = Instantiate(projectile, transform.position, transform.parent.rotation);
                rb.velocity = rb.gameObject.transform.up * 10;
                cooldown01 = 0f;
            } else if (isInUseM2 && cooldown02 >= m2Cooldown) {
                Vector3 euler1 = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z + 30);
                Vector3 euler2 = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z - 30);

                Rigidbody2D rb1 = Instantiate(projectile, transform.position, Quaternion.Euler(euler1));
                Rigidbody2D rb2 = Instantiate(projectile, transform.position, transform.parent.rotation);
                Rigidbody2D rb3 = Instantiate(projectile, transform.position, Quaternion.Euler(euler2));
                rb1.velocity = rb1.gameObject.transform.up * 10;
                rb2.velocity = rb2.gameObject.transform.up * 10;
                rb3.velocity = rb3.gameObject.transform.up * 10;
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
            transform.localPosition = new Vector3(0, 6, 0);
            transform.localScale = new Vector3(30, 30, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 90);

            // Photon ownership transfer on pickup
            photonView.enabled = true;
            photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        }
    }

}
