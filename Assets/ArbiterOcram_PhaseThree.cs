﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbiterOcram_PhaseThree : StateMachineBehaviour {

    public GameObject shockwave;

    private Transform currTransform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        currTransform = animator.transform;
        //StartCoroutine("PhaseShockwave");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    //IEnumerator PhaseShockwave() {
    //    for (int i = 0; i < 3; ++i) {
    //        _ = Instantiate(shockwave, currTransform, Quaternion.identity);
    //    }
    //}
}
