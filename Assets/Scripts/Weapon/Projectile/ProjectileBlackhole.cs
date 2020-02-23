using UnityEngine;

public class ProjectileBlackhole : ProjectileBase {
    float explodeTimer = 0f;
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start() {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        explodeTimer += Time.deltaTime;
        if (explodeTimer >= 5f) {
            myAnimator.SetBool("Explode", true);
        }
    }
}
