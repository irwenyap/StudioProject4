using Photon.Pun;
using UnityEngine;

public class ArbiterOcram_Sync : MonoBehaviourPun, IPunObservable {
    void Start() {
        
    }

    void Update() {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {

        }
    }
}
