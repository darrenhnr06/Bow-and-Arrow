using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public float distance;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("box"))
        {
            Vector3 pos = transform.position;
            pos.z += distance;
            transform.position = pos;
        }
    }
}
