using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 targetOffset;
    public float followSpeed = 2f;

    private Transform myTarget;
    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (myTarget != null)
            myTransform.position = Vector3.Lerp(myTransform.position, myTarget.position + targetOffset, followSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform _transform) {
        myTarget = _transform;
    }
}
