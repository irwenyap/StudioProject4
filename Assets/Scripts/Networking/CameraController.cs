using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Vector3 targetOffset;
    public float followSpeed = 2f;

    [SerializeField]
    private Transform myTarget;
    private Transform myTransform;

    void Start() {
        myTransform = transform;
    }

    void LateUpdate() {
        if (myTarget != null)
            myTransform.position = Vector3.Lerp(myTransform.position, myTarget.position + targetOffset, followSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform _transform) {
        myTarget = _transform;
    }
}
