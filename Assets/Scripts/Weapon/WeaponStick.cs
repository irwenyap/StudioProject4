using Photon.Pun;
using UnityEngine;


public class WeaponStick : WeaponBase, IPunObservable {
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

        // Stats
        attackDamage = 10;
        attackSpeed = 3f;
        deltaTime = attackSpeed;
    }

    void Update() {
        if (isAttached) {
            photonView.enabled = true;
            deltaTime += Time.deltaTime;
            // Direction
            //if ()

            // Shooting
            if (photonView.IsMine) {
                if (Input.GetMouseButton(0))
                    isInUse = true;
                else
                    isInUse = false;

                if (Input.GetKeyDown(KeyCode.Q))
                    WeaponDropped();
            }

            if (isInUse && deltaTime >= attackSpeed) {
                Rigidbody2D rb = Instantiate(projectile, transform.position, projDir.rotation);
                rb.velocity = rb.gameObject.transform.up * 10;
                deltaTime = 0f;
            }
        }
    }

    public void WeaponDropped() {
        base.photonView.RPC("RPC_ThrowWeapon", RpcTarget.All, GetComponentInParent<PlayerController>().dir.normalized);
        //myPlayer.weaponOnHand = null;
        //myPlayer = null;
        //projDir = null;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(isInUse);
        }
        else {
            isInUse = (bool)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.GetComponent<PlayerController>().weaponIsOnHand) {
            myPlayer = collision.GetComponent<PlayerController>();
            //myPlayer.weaponIsOnHand = true;
            transform.SetParent(collision.transform);
            projDir = transform.parent.Find("Weapon");
            WeaponOnHand(true);

            // Setting the item's transform when attached
            transform.localPosition = new Vector3(-0.5f, 0.1f, -1);
            transform.localScale = new Vector3(3, 3, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 45);
            //isAttached = true;
            photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        }
    }
}
