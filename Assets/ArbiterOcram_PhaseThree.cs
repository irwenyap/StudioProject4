using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbiterOcram_PhaseThree : StateMachineBehaviour {
    private ArbiterOcram currAI;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        currAI = animator.GetComponent<ArbiterOcram>();
        currAI.ThirdPhaseCoroutine();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }
}
