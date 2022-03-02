using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothFactor;
    public Vector3 offSet;
    private Vector3 pos;

    private void Update()
    {
        Vector3 targetPos = target.position + offSet;
        pos = Vector3.Lerp(transform.position, targetPos, smoothFactor);
    }

    private void LateUpdate()
    {
        transform.position = pos;
    }
}
