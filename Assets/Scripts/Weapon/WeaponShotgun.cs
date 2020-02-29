using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponShotgun : WeaponBase, IPunObservable {
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

    // Update is called once per frame
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
                //for (int i = 0; i < 5; ++i) {
                //}
                {
                    Vector3 pos = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.2f);
                    Vector3 euler = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z + 5);
                    Rigidbody2D rb = Instantiate(projectile, pos, Quaternion.Euler(euler));
                    rb.velocity = rb.gameObject.transform.up * 10;
                }

                {
                    Vector3 pos = new Vector2(transform.position.x, transform.position.y);
                    Vector3 euler = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z);
                    Rigidbody2D rb = Instantiate(projectile, pos, Quaternion.Euler(euler));
                    rb.velocity = rb.gameObject.transform.up * 10;
                }

                {
                    Vector3 pos = new Vector2(transform.position.x, transform.position.y - 0.3f);
                    Vector3 euler = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z + 4);
                    Rigidbody2D rb = Instantiate(projectile, pos, Quaternion.Euler(euler));
                    rb.velocity = rb.gameObject.transform.up * 10;
                }

                {
                    Vector3 pos = new Vector2(transform.position.x - 0.1f, transform.position.y);
                    Vector3 euler = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z - 2);
                    Rigidbody2D rb = Instantiate(projectile, pos, Quaternion.Euler(euler));
                    rb.velocity = rb.gameObject.transform.up * 10;
                }

                {
                    Vector3 pos = new Vector2(transform.position.x + 0.2f, transform.position.y - 0.5f);
                    Vector3 euler = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z - 3);
                    Rigidbody2D rb = Instantiate(projectile, pos, Quaternion.Euler(euler));
                    rb.velocity = rb.gameObject.transform.up * 10;
                }
                cooldown01 = 0f;
            }
            else if (isInUseM2 && cooldown02 >= m2Cooldown) {
                StartCoroutine("SuccessionShot");
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

    IEnumerator SuccessionShot() {
        for (int i = 0; i < 2; ++i) {
            {
                Vector3 pos = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.2f);
                Vector3 euler = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z + 5);
                Rigidbody2D rb = Instantiate(projectile, pos, Quaternion.Euler(euler));
                rb.velocity = rb.gameObject.transform.up * 10;
            }

            {
                Vector3 pos = new Vector2(transform.position.x, transform.position.y);
                Vector3 euler = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z);
                Rigidbody2D rb = Instantiate(projectile, pos, Quaternion.Euler(euler));
                rb.velocity = rb.gameObject.transform.up * 10;
            }

            {
                Vector3 pos = new Vector2(transform.position.x, transform.position.y - 0.3f);
                Vector3 euler = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z + 4);
                Rigidbody2D rb = Instantiate(projectile, pos, Quaternion.Euler(euler));
                rb.velocity = rb.gameObject.transform.up * 10;
            }

            {
                Vector3 pos = new Vector2(transform.position.x - 0.1f, transform.position.y);
                Vector3 euler = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z - 2);
                Rigidbody2D rb = Instantiate(projectile, pos, Quaternion.Euler(euler));
                rb.velocity = rb.gameObject.transform.up * 10;
            }

            {
                Vector3 pos = new Vector2(transform.position.x + 0.2f, transform.position.y - 0.5f);
                Vector3 euler = new Vector3(transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z - 3);
                Rigidbody2D rb = Instantiate(projectile, pos, Quaternion.Euler(euler));
                rb.velocity = rb.gameObject.transform.up * 10;
            }
            yield return new WaitForSeconds(0.5f);
        }
        cooldown02 = 0f;
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
            photonView.enabled = true;
            photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
        }
    }
}
