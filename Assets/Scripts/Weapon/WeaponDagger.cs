using Photon.Pun;
using UnityEngine;

public class WeaponDagger : WeaponBase, IPunObservable {
    public Rigidbody2D projectile;

    private SpriteRenderer weaponSprite;
    private Transform location;

    void Start() {
        // Disable Photon View on ground to save bandwidth
        photonView.enabled = false;

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
            if (photonView.IsMine) {
            }

            if (isInUseM1) {
                var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                var towards = transform.position + (5 * dir.normalized);
                transform.position = Vector3.MoveTowards(transform.position, towards, Time.deltaTime);
                
                //Rigidbody2D rb = Instantiate(projectile, transform.position, transform.parent.rotation);
                //rb.velocity = rb.gameObject.transform.up * 10;
                //fireRate = 0f;
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
            transform.SetParent(collision.transform);
            WeaponOnHand(true);

            // Setting the item's transform when attached
            transform.localPosition = new Vector3(0.2f, -0.15f, -1);
            transform.localScale = new Vector3(5, 5, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 45);

            // Photon ownership transfer on pickup
            photonView.enabled = true;
            photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        }
    }
}
