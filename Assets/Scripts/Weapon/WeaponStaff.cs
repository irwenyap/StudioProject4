using Photon.Pun;
using UnityEngine;


public class WeaponStaff : WeaponBase, IPunObservable {
    public Rigidbody2D projectile;
    public SpriteRenderer weaponSprite;

    private Transform projDir;
    private bool isShooting = false;
    private float fireRate = 0f;

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
            // Direction
            //if ()

            // Shooting
            if (photonView.IsMine) {
                if (Input.GetMouseButton(0))
                    isShooting = true;
                else
                    isShooting = false;
            }
            fireRate += Time.deltaTime;
        }


        if (isShooting && fireRate >= 1f) {
            Rigidbody2D rb = Instantiate(projectile, transform.position, projDir.rotation);
            rb.velocity = rb.gameObject.transform.up * 10;
            fireRate = 0f;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(isShooting);
        }
        else {
            isShooting = (bool)stream.ReceiveNext();
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
