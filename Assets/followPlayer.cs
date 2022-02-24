using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public float smoothTime = 0.3f;
    public Transform followTransform;

    public float smoothSpeed = 0.01f;

    public Vector3 offset;

    public Transform parentCamera;

    // Update is called once per frame
    void FixedUpdate()
    {
        /*Vector3 desiredPos = followTransform.position;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;*/
        parentCamera.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);

    }



}
