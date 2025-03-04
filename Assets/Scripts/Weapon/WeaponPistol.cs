﻿using Photon.Pun;
using UnityEngine;

public class WeaponPistol : WeaponBase, IPunObservable {
    public Rigidbody2D projectile;
    public SpriteRenderer weaponSprite;

    private bool isShooting = false;
    private float fireRate = 2f;

    void Start() {
        WeaponID = 0;
        weaponSprite = GetComponent<SpriteRenderer>();
        photonView.enabled = false;
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
        if (collision.GetComponent<PlayerController>().weaponOnHand == null) {
            collision.GetComponent<PlayerController>().weaponOnHand = gameObject;
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<BoxCollider2D>().enabled = false;
            transform.SetParent(collision.transform.Find("Weapon"));
            transform.localPosition = new Vector3(0, 1, 0);
            transform.localScale = new Vector3(2, 2, 2);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
            photonView.TransferOwnership(collision.GetComponent<PhotonView>().Owner);
            isAttached = true;
        }
    }
}
