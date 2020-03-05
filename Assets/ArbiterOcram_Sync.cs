using Photon.Pun;
using UnityEngine;

public class ArbiterOcram_Sync : MonoBehaviourPun, IPunObservable {

    private AI_Base myAI;
    private Animator myAnimator;
    private Vector3 latestPos;

    public MonoBehaviour[] localScripts;
    public Collider2D[] localColliders;

    void Start() {
        myAnimator = GetComponent<Animator>();
        if (!PhotonNetwork.IsMasterClient) {
            for (int i = 0; i < localScripts.Length; ++i) {
                localScripts[i].enabled = false;
            }
            for (int i = 0; i < localColliders.Length; ++i) {
                localColliders[i].enabled = false;
            }
        }
        else {
            myAI = GetComponent<AI_Base>();
        }
    }

    private void FixedUpdate() {
        // Animation for other Clients
        if (!photonView.IsMine) {
            transform.position = Vector3.MoveTowards(transform.position, latestPos, Time.deltaTime * 5);
            //transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            //transform.position = latestPos;
            //transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime * 5);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(transform.position);
            stream.SendNext(myAnimator.GetBool("isPhase1"));
            stream.SendNext(myAnimator.GetBool("isPhase2"));
            stream.SendNext(myAnimator.GetBool("isPhase3"));
            stream.SendNext(myAnimator.GetBool("isPhase1ShardStorm"));
            stream.SendNext(myAnimator.GetBool("activateSkullshotStorm"));
        } else {
            latestPos = (Vector3)stream.ReceiveNext();
            myAnimator.SetBool("isPhase1", (bool)stream.ReceiveNext());
            myAnimator.SetBool("isPhase2", (bool)stream.ReceiveNext());
            myAnimator.SetBool("isPhase3", (bool)stream.ReceiveNext());
            myAnimator.SetBool("isPhase1ShardStorm", (bool)stream.ReceiveNext());
            myAnimator.SetBool("activateSkullshotStorm", (bool)stream.ReceiveNext());
        }
    }
}
