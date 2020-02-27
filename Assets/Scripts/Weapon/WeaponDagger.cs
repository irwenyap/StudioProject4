using Photon.Pun;
using UnityEngine;

public class WeaponDagger : WeaponBase, IPunObservable {
    public Rigidbody2D projectile;
    public SpriteRenderer weaponSprite;

    private Transform location;
    private bool isShooting = false;
    private float fireRate = 0f;

    void Start() {
        weaponSprite = GetComponent<SpriteRenderer>();
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
        }

        //fireRate += Time.deltaTime;

        if (isShooting) {
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var towards = transform.position + (5 * dir.normalized);
            transform.position = Vector3.Lerp(transform.position, dir.normalized, 5 * Time.deltaTime);

            //Rigidbody2D rb = Instantiate(projectile, transform.position, transform.parent.rotation);
            //rb.velocity = rb.gameObject.transform.up * 10;
            //fireRate = 0f;
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
        if (!collision.GetComponent<PlayerController>().weaponIsOnHand) {
            collision.GetComponent<PlayerController>().weaponIsOnHand = true;
            location = collision.transform.Find("Weapon");
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<BoxCollider2D>().enabled = false;
            transform.SetParent(collision.transform);
            transform.localPosition = new Vector3(0.2f, -0.15f, -1);
            transform.localScale = new Vector3(0.3f, 0.3f, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 45);
            isAttached = true;
            photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        }
    }
}
