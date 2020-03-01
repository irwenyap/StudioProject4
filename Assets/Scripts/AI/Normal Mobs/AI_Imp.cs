using Photon.Pun;
using UnityEngine;

public class AI_Imp : AI_Base, IPunObservable {
    private CircleCollider2D myCircleCollider;

    void Start() {

        myCircleCollider = GetComponent<CircleCollider2D>();

        // Stats
        maxHealth = 50f;
        currHealth = maxHealth;
        moveSpeed = 5f;
    }

    void Update() {
        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 2 * Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(transform.position);
        } else {
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            target = collision.transform;
            Destroy(myCircleCollider);
        }
    }
}
