using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSync : MonoBehaviourPun, IPunObservable
{
    Vector3 latestPos;
    Quaternion latestRot;

    private Animator myAnimator;
    private PlayerController myPlayer;
    [SerializeField]
    private Transform weaponLocationData = null;

    public MonoBehaviour[] localScripts;
    
    private void Start() {
        myAnimator = GetComponent<Animator>();
        myPlayer = GetComponent<PlayerController>();
        if (photonView.IsMine) {
        } else {
            for (int i = 0; i < localScripts.Length; ++i) {
                localScripts[i].enabled = false;
            }
        }
    }

    private void FixedUpdate() {
        // Animation for other Clients
        if (!photonView.IsMine) {
            if (latestPos != transform.position) {
                int dir = latestPos.x < transform.position.x ? -1 : 1;
                myAnimator.SetFloat("moveX", dir);
            }
            transform.position = Vector3.MoveTowards(transform.position, latestPos, Time.deltaTime * 5);
            //transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            //transform.position = latestPos;
            //transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime * 5);
            transform.rotation = latestRot;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(weaponLocationData.rotation);
            //stream.SendNext(myPlayer.weaponIsOnHand);
        }
        else {
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();
            weaponLocationData.rotation = (Quaternion)stream.ReceiveNext();
            //myPlayer.weaponIsOnHand = (bool)stream.ReceiveNext();
        }
    }
}
