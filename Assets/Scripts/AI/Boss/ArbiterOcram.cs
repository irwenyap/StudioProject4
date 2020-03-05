using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbiterOcram : AI_Base {
    private Animator myAnimator;

    public GameObject shockwave;

    private Transform destination;

    private float phaseOneAttack = 0f;

    void Start() {
        maxHealth = 1200f;
        currHealth = maxHealth;
        myAnimator = GetComponent<Animator>();
    }

    void Update() {
        if (myAnimator.GetBool("isPhase1")) {
            phaseOneAttack += Time.deltaTime;

            //if (phaseOneAttack)


        }



    }

    public void ThirdPhaseCoroutine() {
        StartCoroutine("PhaseShockwave");
    }

    IEnumerator PhaseShockwave() {
        for (int i = 0; i < 3; ++i) {
            _ = Instantiate(shockwave, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.8f);
        }
    }
}
