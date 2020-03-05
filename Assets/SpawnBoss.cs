using Photon.Pun;
using UnityEngine;

public class SpawnBoss : MonoBehaviour {

    public Transform pos;

    private void OnTriggerEnter2D(Collider2D collision) {
        PhotonNetwork.InstantiateSceneObject("Prefabs/AIs/Arbiter Ocram", pos.position, Quaternion.identity);
        AudioManager.instance.StopPlaying("Theme");
        AudioManager.instance.Play("Final Boss Theme");

        Destroy(gameObject);
    }
}
