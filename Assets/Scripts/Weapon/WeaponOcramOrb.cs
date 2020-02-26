using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOcramOrb : MonoBehaviourPun, IPunObservable {
    public Rigidbody2D projectile;
    public PlayerList list;
    public GameObject player;

    private bool isShooting = true;
    float deltaTime = 0f;
    float fireRate = 1f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (isShooting) {
            deltaTime += Time.deltaTime;
            int random = Random.Range(0, list.playerList.Length);
            if (deltaTime > fireRate) {
                Vector3 dir = list.playerList[random].transform.position - transform.position;
                //Vector3 dir = player.transform.position - transform.position;
                //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //Rigidbody2D rb = Instantiate(projectile, transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                Rigidbody2D rb = Instantiate(projectile, transform.position, Quaternion.LookRotation(Vector3.forward, dir));
                //rb.transform.LookAt(player.transform);
                rb.velocity = rb.transform.up * 10;
                deltaTime = 0f;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            //stream.SendNext(isShooting);
        }
        else {
            //isShooting = (bool)stream.ReceiveNext();
        }
    }
}
