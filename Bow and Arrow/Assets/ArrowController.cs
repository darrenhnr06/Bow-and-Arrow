using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Destroy(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Destroy(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
    }

    private void Destroy(Collider other)
    {
        if(other.gameObject.CompareTag("box"))
        {
            Destroy(gameObject);
        }
    }
}
