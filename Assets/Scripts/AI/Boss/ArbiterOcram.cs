using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbiterOcram : AI_Base {
    private Animator myAnimator;

    void Start() {
        maxHealth = 1200f;
        currHealth = maxHealth;
        myAnimator = GetComponent<Animator>();
    }

    void Update() {
        
        

    }
}
