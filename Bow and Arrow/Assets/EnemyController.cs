using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    private Vector3 dir;

    public GameObject player;

    public GameObject enemyRow;

    private void Awake()
    {
       dir = transform.forward;
    }


    private void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }


    private void OnTriggerExit(Collider other)
    {
        DestroyEnemy(other);
    }

    private void OnTriggerStay(Collider other)
    {
        DestroyEnemy(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyEnemy(other);
    }

    private void DestroyEnemy(Collider other)
    {
        if (other.gameObject.CompareTag("arrow"))
        {
            Destroy(other.gameObject);
            EnemyCount.enemyRowHit++;
            enemyRow.SetActive(false);
        }
    }
}
