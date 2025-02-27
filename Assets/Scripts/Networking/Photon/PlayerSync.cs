﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSync : MonoBehaviourPun, IPunObservable
{
    Vector3 latestPos;
    Quaternion latestRot;

    public Rigidbody2D beam;

    public MonoBehaviour[] localScripts;

    private Animator myAnimator;
    private PlayerController myPC;
    private Transform weaponLocationData;

    private void Start() {
        myAnimator = GetComponent<Animator>();
        weaponLocationData = transform.Find("Weapon");
        if (photonView.IsMine) {
            myPC = GetComponent<PlayerController>();
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
            stream.SendNext(weaponLocationData.rotation);
        }
        else {
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();
            weaponLocationData.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    private void Update() {
        // Animation for other Clients
        if (!photonView.IsMine) {
            if (latestPos != transform.position) {
                int dir = latestPos.x < transform.position.x ? -1 : 1;
                myAnimator.SetFloat("moveX", dir);
            }
            //transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            transform.position = latestPos;
            //transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime * 5);
            transform.rotation = latestRot;
        }
    }
}
