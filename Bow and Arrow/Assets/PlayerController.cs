using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;

    public float speed;
    public GameObject bow;

    public GameObject arrowFirst;
    private GameObject arrowOne;
    private GameObject arrowTwo;
    private GameObject arrowThree;


    public float shootTime;
    private float arrowSpeed;

    private Vector3 dir;
    private Rigidbody arrowRigidbodyOne;
    private Rigidbody arrowRigidbodyTwo;
    private Rigidbody arrowRigidbodyThree;

    public GameObject cube;

    public GameObject arrowTransformOne;
    public GameObject arrowTransformTwo;
    public GameObject arrowTransformThree;

    public GameObject directionCube;

    //public Slider slider;
    private float touchTime;

    public float arrowFactor;
    //private float rotateAngle;

    public GameObject projectionOne;
    public GameObject projectionTwo;
    public GameObject projectionThree;

    private bool countTouch;
    private bool setAngle;

    //public float setTime;
    public float DirCubeInitAngle;

    public float arrowRotation;
    public float arrowRotateDuration;
    public float arrowSpeedModifier;

    public float playerRotateFactor;
    private bool run;

    public float projectionDelay;
    private float projectionDelayTemp;

    private bool delayAnimation;

    public float minArrowSpeed;

    public float maxArrowSpeed;

    public GameObject[] enemiesRow;

    public float playerEnemiesGap;

    public float enemiesGap;

    public float playerEnemyRestrictGap;

    private bool runTimer;

    private void Awake()
    {
        arrowFirst.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        arrowOne = GameObject.Instantiate(arrowFirst, arrowTransformOne.transform.position, arrowTransformOne.transform.rotation, directionCube.transform);
        arrowRigidbodyOne = arrowOne.gameObject.GetComponent<Rigidbody>();
        arrowRigidbodyOne.constraints = RigidbodyConstraints.FreezeAll;

        arrowTwo = GameObject.Instantiate(arrowFirst, arrowTransformTwo.transform.position, arrowTransformTwo.transform.rotation, directionCube.transform);
        arrowRigidbodyTwo = arrowTwo.gameObject.GetComponent<Rigidbody>();
        arrowRigidbodyTwo.constraints = RigidbodyConstraints.FreezeAll;

        arrowThree = GameObject.Instantiate(arrowFirst, arrowTransformThree.transform.position, arrowTransformThree.transform.rotation, directionCube.transform);
        arrowRigidbodyThree = arrowThree.gameObject.GetComponent<Rigidbody>();
        arrowRigidbodyThree.constraints = RigidbodyConstraints.FreezeAll;

        touchTime = 0;
        countTouch = true;
        setAngle = false;

        directionCube.transform.eulerAngles = new Vector3(DirCubeInitAngle, 0, 0);
        dir = directionCube.transform.forward;

        run = true;
        arrowSpeed = minArrowSpeed;

        delayAnimation = true;
        runTimer = false;

        projectionDelayTemp = projectionDelay;

    }

    private void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Vector3 pos = transform.position + new Vector3(0, 0, 10);
        //enemy.gameObject.transform.position = pos;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && countTouch == true)
        {
            run = false;
            rb.velocity = transform.forward * 0;

            rb.constraints = RigidbodyConstraints.None;
            transform.eulerAngles = new Vector3(0, playerRotateFactor, 0);
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            bow.gameObject.SetActive(true);

            arrowOne.gameObject.SetActive(true);

            //animator.SetBool("shoot", true);
            //StartCoroutine(InitiateShoot());


            arrowRigidbodyOne.constraints = RigidbodyConstraints.None;
            arrowRigidbodyTwo.constraints = RigidbodyConstraints.None;
            arrowRigidbodyThree.constraints = RigidbodyConstraints.None;

            animator.SetBool("shoot", true);
            //runTimer = true;


            StartCoroutine(TouchTimer());
        }

        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            setAngle = false;
        }

        if (run == true)
        {
            rb.velocity = transform.forward * speed;
        }

        if (EnemyCount.enemyRowHit >= 12)
        {
            SpawnEnemy();
            EnemyCount.enemyRowHit = 0;
        }

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, playerEnemyRestrictGap))
        {
            if (hit.collider.CompareTag("box"))
            {
                speed = 4.2f;
            }
            else
            {
                speed = 6;
            }
        }
        else
        {
            speed = 6;
        }

        if (arrowSpeed <= 90f)
        {
            Physics.gravity = new Vector3(0, -150, 0);
            arrowRotateDuration = 0.5f;
        }
        else
        {
            Physics.gravity = new Vector3(0, -50, 0);
            arrowRotateDuration = 1.5f;
        }

        /*if (runTimer == true)
        {
            projectionDelay -= Time.deltaTime;

            if (projectionDelay <= 0)
            {
                animator.speed = 0;
                runTimer = false;
                StartCoroutine(TouchTimer());
            }
        }*/

        if(Input.touchCount == 0)
        {
            setAngle = false;
        }
    }

    IEnumerator InitiateShoot()
    {
        yield return new WaitForSeconds(0.6f);
        if (delayAnimation == true)
        {
            animator.speed = 0;
        }

    }

    IEnumerator SetAnimation()
    {
        yield return new WaitForSeconds(1f);

    }

    IEnumerator TouchTimer()
    {
        animator.SetBool("shoot", true);
        StartCoroutine(InitiateShoot());
        //yield return new WaitForSeconds(0.6f);
        //animator.speed = 0;

        setAngle = true;
        countTouch = false;
        int k = 0;



        projectionOne.gameObject.SetActive(true);
        projectionTwo.gameObject.SetActive(true);
        projectionThree.gameObject.SetActive(true);

        
         while (k <= projectionDelay)
         {
             touchTime += 0.005f;
             arrowSpeed += touchTime * arrowFactor;
             k++;
             yield return new WaitForSeconds(0.1f);

             if(setAngle == false)
             {
                 delayAnimation = false;
                 goto abc;
             }
          }

        Debug.Log(arrowSpeed);


        while (setAngle == true)
        {
            Debug.Log("abcd");
            if (setAngle == true)
            {
                touchTime += 0.005f;
                arrowSpeed += touchTime * arrowFactor;

                if(arrowSpeed >= maxArrowSpeed)
                {
                    break;
                }
               


                //Debug.Log(arrowSpeed);
                //projection.gameObject.SetActive(true);
                k++;
            }

            else
            {
                break;
            }
            
            yield return new WaitForSeconds(0.1f);
        }

    abc:
        

        projectionOne.gameObject.SetActive(false);
        projectionTwo.gameObject.SetActive(false);
        projectionThree.gameObject.SetActive(false);

        animator.speed = 1;
        touchTime = 0;
        StartCoroutine(ShootArrow());
    }

   
    IEnumerator ShootArrow()
    {
        //rb.constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(shootTime);

        arrowOne.transform.SetParent(null);
        arrowRigidbodyOne.velocity = dir * arrowSpeed * arrowSpeedModifier;
        arrowRigidbodyOne.useGravity = true;
        arrowOne.transform.DORotate(new Vector3(arrowRotation, 0, 0), arrowRotateDuration);

        arrowTwo.gameObject.SetActive(true);
        arrowTwo.transform.SetParent(null);
        arrowRigidbodyTwo.velocity = dir * arrowSpeed * arrowSpeedModifier;
        arrowRigidbodyTwo.useGravity = true;
        arrowTwo.transform.DORotate(new Vector3(arrowRotation, 0, 0), arrowRotateDuration);

        arrowThree.gameObject.SetActive(true);
        arrowThree.transform.SetParent(null);
        arrowRigidbodyThree.velocity = dir * arrowSpeed * arrowSpeedModifier;
        arrowRigidbodyThree.useGravity = true;
        arrowThree.transform.DORotate(new Vector3(arrowRotation, 0, 0), arrowRotateDuration);


        arrowOne = GameObject.Instantiate(arrowFirst, arrowTransformOne.transform.position, arrowTransformOne.transform.rotation, directionCube.transform);
        arrowRigidbodyOne = arrowOne.gameObject.GetComponent<Rigidbody>();
        arrowRigidbodyOne.constraints = RigidbodyConstraints.FreezeAll;

        arrowTwo = GameObject.Instantiate(arrowFirst, arrowTransformTwo.transform.position, arrowTransformTwo.transform.rotation, directionCube.transform);
        arrowRigidbodyTwo = arrowTwo.gameObject.GetComponent<Rigidbody>();
        arrowRigidbodyTwo.constraints = RigidbodyConstraints.FreezeAll;

        arrowThree = GameObject.Instantiate(arrowFirst, arrowTransformThree.transform.position, arrowTransformThree.transform.rotation, directionCube.transform);
        arrowRigidbodyThree = arrowThree.gameObject.GetComponent<Rigidbody>();
        arrowRigidbodyThree.constraints = RigidbodyConstraints.FreezeAll;

        rb.constraints = RigidbodyConstraints.None;
        transform.eulerAngles = new Vector3(0, 0, 0);
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        bow.gameObject.SetActive(false);

        arrowOne.gameObject.SetActive(false);
        arrowTwo.gameObject.SetActive(false);
        arrowThree.gameObject.SetActive(false);

        animator.SetBool("shoot", false);

        run = true;
        arrowSpeed = minArrowSpeed;
        projectionDelay = projectionDelayTemp;
        StartCoroutine(StartCountTouch());
    }

    IEnumerator StartCountTouch()
    {
        yield return new WaitForSeconds(0.5f);
        countTouch = true;
        delayAnimation = true;
    }

    public Vector3 ReturnDir()
    {
        return dir;
    }

    public float ReturnArrowSpeed()
    {
        return arrowSpeed;
    }

    public void SpawnEnemy()
    {
        
        for(int i=0; i < enemiesRow.Length; i++)
        {
            enemiesRow[i].gameObject.SetActive(true);
            for (int j = 0; j < enemiesRow[i].transform.childCount; j++)
            {
                Vector3 pos = new Vector3(enemiesRow[i].transform.GetChild(j).transform.position.x, transform.position.y, transform.position.z + playerEnemiesGap + (enemiesGap * (i + 1)));
                enemiesRow[i].transform.GetChild(j).transform.position = pos;
            }
        }
    }

}
