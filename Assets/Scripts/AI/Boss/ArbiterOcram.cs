using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbiterOcram : AI_Base {

    private Animator myAnimator;

    public GameObject shockwave;

    public Vector2 destination;

    public float moveTime = 0f;
    private float phaseOneAttack = 0f;
    private float phaseTwoAttack = 0f;

    void Start() {
        maxHealth = 10000f;
        currHealth = maxHealth;
        myAnimator = GetComponent<Animator>();
        destination = transform.position;
    }

    void Update() {
        if (myAnimator.GetBool("isPhase1")) {
            phaseOneAttack += Time.deltaTime;
            moveTime += Time.deltaTime;

            if (moveTime >= 10f) {
                destination = new Vector2(transform.position.x + Random.Range(-10, 10), transform.position.y + Random.Range(-10, 10));
                moveTime = 0;
            }

            if (Vector2.Distance(destination, transform.position) >= 2f) {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 10);
            }

            if (phaseOneAttack >= 15f) {
                int random = Random.Range(0, 2);

                if (random == 0) {
                    myAnimator.SetBool("isPhase1ShardStorm", true);
                } else {
                    myAnimator.SetBool("activateSkullshotStorm", true);
                }
                phaseOneAttack = 0f;
            }

            if (currHealth < 7000) {
                myAnimator.SetBool("isPhase2", true);
                myAnimator.SetBool("isPhase1", false);
            }
        } else if (myAnimator.GetBool("isPhase2")) {
            phaseTwoAttack += Time.deltaTime;
            moveTime += Time.deltaTime;

            if (moveTime >= 10f) {
                destination = new Vector2(transform.position.x + Random.Range(-10, 10), transform.position.y + Random.Range(-10, 10));
            }

            if (Vector2.Distance(destination, transform.position) >= 2f) {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 10);
            }

            if (phaseOneAttack >= 8f) {
                myAnimator.SetBool("activatePhase2SkullshotStorm", true);
                phaseOneAttack = 0f;
            }

            if (currHealth < 4000) {
                myAnimator.SetBool("isPhase3", true);
                myAnimator.SetBool("isPhase2", false);
            }
        } else if (myAnimator.GetBool("isPhase3")) {

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
