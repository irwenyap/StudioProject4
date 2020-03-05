using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour {

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.05f;
    private float dampingSpeed = 1.0f;

    void Update() {
        if (shakeDuration > 0) {
            transform.localPosition = transform.localPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else {
            shakeDuration = 0f;
        }
    }

    public void TriggerShake(float duration) {
        shakeDuration = duration;
    }
}
