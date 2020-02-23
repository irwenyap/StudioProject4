using Photon.Pun;
using UnityEngine;


public class WeaponStaff : WeaponBase, IPunObservable {
    public Rigidbody2D projectile;
    public SpriteRenderer weaponSprite;

    private bool isShooting = false;
    private float fireRate = 0f;

    //private BoxCollider2D myCollider;
    //private Rigidbody2D myRigidbody;

    void Start() {
        WeaponID = 0;
        weaponSprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {

        if (isAttached) {
            // Direction


            // Shooting
            if (Input.GetMouseButton(0))
                isShooting = true;
            else
                isShooting = false;
            if (photonView.IsMine) {
            }
            fireRate += Time.deltaTime;
        }


        if (isShooting && fireRate >= 1f) {
            Rigidbody2D rb = Instantiate(projectile, transform.position, transform.parent.rotation);
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
        if (collision.GetComponent<PlayerController>().weaponOnHand == null) {
            collision.GetComponent<PlayerController>().weaponOnHand = gameObject;
            WeaponOnHand(true);
            transform.SetParent(collision.transform);
            // Setting the item's transform on hand
            transform.localPosition = new Vector3(-0.5f, 0.1f, -1);
            transform.localScale = new Vector3(3, 3, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 45);
            //isAttached = true;
            //photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        }
    }
}
