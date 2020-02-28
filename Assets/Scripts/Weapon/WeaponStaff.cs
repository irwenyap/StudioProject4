using Photon.Pun;
using UnityEngine;

public class WeaponStaff : WeaponBase, IPunObservable {
    public Rigidbody2D projectile;

    private SpriteRenderer weaponSprite;
    private Transform projDir;

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
        cooldown01 = attackSpeed;
    }

    void Update() {
        if (isAttached) {
            photonView.enabled = true;
            cooldown01 += Time.deltaTime;

            // Direction
            //if ()

            // Shooting
            if (photonView.IsMine) {
                if (Input.GetMouseButton(0))
                    isInUseM1 = true;
                else
                    isInUseM1 = false;

                if (Input.GetKeyDown(KeyCode.Q))
                    DropWeapon();
            }

            if (isInUseM1 && cooldown01 >= attackSpeed) {
                Rigidbody2D rb = Instantiate(projectile, transform.position, projDir.rotation);
                rb.velocity = rb.gameObject.transform.up * 10;
                cooldown01 = 0f;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(isInUseM1);
        }
        else {
            isInUseM1 = (bool)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //if (collision.GetComponent<PlayerController>().weaponOnHand == null) {
        //    collision.GetComponent<PlayerController>().weaponOnHand = this;
        //    transform.SetParent(collision.transform);
        //    projDir = transform.parent.Find("Weapon");
        //    WeaponOnHand(true);

        //    // Setting the item's transform when attached
        //    transform.localPosition = new Vector3(-0.5f, 0.1f, -1);
        //    transform.localScale = new Vector3(2.5f, 2.5f, 1);
        //    transform.localRotation = Quaternion.Euler(0, 0, 45);
        //    //isAttached = true;
        //    //photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        //}
    }
}
