using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbiterOcram_Phase2SkullshotStorm : StateMachineBehaviour {

    float bounceTime;

    [SerializeField]
    private Rigidbody2D skullShot;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        bounceTime = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        bounceTime += Time.deltaTime;

        if (bounceTime >= 1f) {
            float angle = 0;
            for (int i = 0; i < 18; ++i) {
                Rigidbody2D rb = Instantiate(skullShot, animator.transform.position, Quaternion.Euler(0, 0, angle));
                rb.velocity = rb.transform.up * 10;
                angle += 20;
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }
}
