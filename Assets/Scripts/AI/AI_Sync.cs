using Photon.Pun;
using UnityEngine;

public class AI_Sync : MonoBehaviourPun, IPunObservable {

    private Vector3 latestPos;

    public MonoBehaviour[] localScripts;
    public Collider2D[] localColliders;

    private void Start() {
        if (!PhotonNetwork.IsMasterClient) {
            for (int i = 0; i < localScripts.Length; ++i) {
                localScripts[i].enabled = false;
            }
            for (int i = 0; i < localColliders.Length; ++i) {
                localColliders[i].enabled = false;
            }
        }
    }

    private void FixedUpdate() {
        if (!PhotonNetwork.IsMasterClient) {
            transform.position = latestPos;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(transform.position);
        }
        else {
            latestPos = (Vector3)stream.ReceiveNext();
        }
    }
}
