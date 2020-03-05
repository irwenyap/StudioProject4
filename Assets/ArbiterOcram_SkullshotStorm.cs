using Photon.Pun;
using UnityEngine;

public class ArbiterOcram_SkullshotStorm : StateMachineBehaviour {
    float angle;
    float bounceTime;
    bool angleCheck = false;

    float skillActiveTime;

    [SerializeField]
    private Rigidbody2D skullShot;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        angle = 0;
        skillActiveTime = 0;
        angleCheck = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (angle <= 360 && !angleCheck) {
            angle += Time.deltaTime * 75;
        } else if (angle >= 0) {
            angle -= Time.deltaTime * 75;
            angleCheck = true;
        } else {
            animator.SetBool("activateSkullshotStorm", false);
        }

        bounceTime += Time.deltaTime;
        skillActiveTime += Time.deltaTime;

        if (bounceTime >= 0.2f) {
            Rigidbody2D rb = Instantiate(skullShot, animator.transform.position, Quaternion.Euler(0, 0, angle));
            rb.velocity = rb.transform.up * 5;
            bounceTime = 0f;
        }

        //if (skillActiveTime >= 15f) {
        //    
        //}
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }
}
