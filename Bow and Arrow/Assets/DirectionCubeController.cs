using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCubeController : MonoBehaviour
{
    public GameObject playerGameobject;

    void Update()
    {
        Vector3 targetPos = playerGameobject.transform.position;
        transform.position = targetPos;
    }
}
