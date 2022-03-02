using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    private Vector3 dir;

    public GameObject player;

    private void Awake()
    {
       dir = transform.forward;
    }


    private void Update()
    {
        //rb.velocity = (dir * speed); //+ (Vector3.up * Physics.gravity.y * 0.2f);
        //rb.velocity = (dir * speed);
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
            //player.GetComponent<PlayerController>().SpawnEnemy(gameObject);
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
