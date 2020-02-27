using Photon.Pun;
using UnityEngine;

public class WeaponPistol : WeaponBase, IPunObservable {
    public Rigidbody2D projectile;

    private SpriteRenderer weaponSprite;
    private bool isShooting = false;
    private float fireRate = 2f;

    void Start() {
        // Disable Photon View on ground to save bandwidth
        photonView.enabled = false;
        // Reference to component
        weaponSprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (isAttached) {
            photonView.enabled = true;
            // Rotation of Gun
            if (transform.parent.eulerAngles.z > 0 && transform.parent.eulerAngles.z < 180)
                weaponSprite.flipY = true;
            else
                weaponSprite.flipY = false;

            // Shooting
            if (photonView.IsMine) {
                if (Input.GetMouseButton(0))
                    isShooting = true;
                else
                    isShooting = false;
            }
            fireRate += Time.deltaTime;
        }


        if (isShooting && fireRate >= 2f) {
            Rigidbody2D rb = Instantiate(projectile, transform.position + (transform.right * 0.5f), transform.parent.rotation);
            rb.velocity = rb.gameObject.transform.up * 10;
            fireRate = 0f;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(isShooting);
            Debug.Log("Pistol: Sending Info");
        }
        else {
            isShooting = (bool)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.GetComponent<PlayerController>().weaponIsOnHand) {
            collision.GetComponent<PlayerController>().weaponIsOnHand = true;
            transform.SetParent(collision.transform.Find("Weapon"));
            WeaponOnHand(true);

            // Setting the item's transform when attached
            transform.localPosition = new Vector3(0, 4, -1);
            transform.localScale = new Vector3(15, 15, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 90);

            // Photon ownership transfer on pickup
            //photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        }
    }
}
