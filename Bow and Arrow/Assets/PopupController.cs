using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject enemy;

    void Update()
    {
        Vector3 targetPos = enemy.transform.position;
        transform.position = targetPos;
    }
}
