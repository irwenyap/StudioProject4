using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSync : MonoBehaviourPun, IPunObservable
{
    Vector3 latestPos;
    Quaternion latestRot;
    bool isShooting = false;

    public Rigidbody2D beam;

    public MonoBehaviour[] localScripts;

    private void Start() {
        if (photonView.IsMine) {

        } else {
            for (int i = 0; i < localScripts.Length; ++i) {
                localScripts[i].enabled = false;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(isShooting);
        }
        else {
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();
            isShooting = (bool)stream.ReceiveNext();
        }
    }

    private void Update() {

        if (photonView.IsMine) {
            if (Input.GetMouseButtonDown(0))
                isShooting = true;
            else if (Input.GetMouseButtonUp(0))
                isShooting = false;
        }

        if (!photonView.IsMine) {
            transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime * 5);
        }

        if (isShooting) {
            Rigidbody2D rb = Instantiate(beam, transform.position + (transform.up * 0.5f), transform.rotation);
            rb.velocity = rb.gameObject.transform.up * 10;
        }
    }
}
