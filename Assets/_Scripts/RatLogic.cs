using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Rigidbody2D))]
public class RatLogic : MonoBehaviour
{
    public float moveSpeed, detectionRadius, thresholdDistance, randomPointDuration, randomPointTime, rotationSpeed;
    bool isRunning = false;
    Vector3 lastSeenScare;
    Vector3 moveDir;
    Coroutine wanderRoutine, runRoutine;
    public Color color;
    public bool debugBool;
    Vector2 targetDirection;
    Rigidbody2D _rigidbody2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomPointTime = randomPointDuration;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // targetRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        targetDirection = transform.up;
        StartCoroutine(WanderTest());
    }

    // Update is called once per frame
    void Update()
    {
        // DoDetection();
    }

    void DoDetection()
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, detectionRadius, transform.up);
        
        if(raycastHit2D.collider != null)
        {
            isRunning = true;
            runRoutine ??= StartCoroutine(Run());
        }
        else
        {
            // Compound assignment?? If wander rountine is null, sets it to new routine. 
            isRunning = false;
            wanderRoutine ??= StartCoroutine(Wander());
            
        }
        

    }
    IEnumerator Run()
    {
        while(isRunning)
        {
            moveDir = -lastSeenScare;
            Move();
            yield return null;
        }
    }
    IEnumerator Wander()
    {
        while(isRunning == false)
        {
            moveDir =  Random.insideUnitCircle;
            Move();
            yield return null;
        }
    }

    IEnumerator WanderTest()
    {
        while(debugBool)
        {
            moveDir =  Vector3.right;
            Move();
            randomPointTime -= Time.deltaTime;
            if(randomPointTime <= 0)
            {
                randomPointTime = randomPointDuration;
                float angleChange = Random.Range(-90f, 90f);
                targetDirection = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;
            }
            yield return null;
        }
    }
    void Move()
    {
        // transform.position = transform.position + (transform.up * moveSpeed * Time.deltaTime);
        // Quaternion rotation = Quaternion.RotateTowards(transform.rotation, LookAt2D(look), rotationSpeed * Time.deltaTime);
        // _rigidbody2D.SetRotation(rotation);
        _rigidbody2D.linearVelocity = (transform.up * moveSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }

    static Quaternion LookAt2D(Transform target, Transform myTransform)
    {
        Quaternion rotation = Quaternion.LookRotation(
        target.transform.position - myTransform.position,
        myTransform.TransformDirection(Vector3.up)
        );

        Quaternion rotation2D = new Quaternion(0, 0, rotation.z, rotation.w);
        return rotation2D;
    }
    
    static Quaternion LookAt2D(Vector3 targetPos, Transform myTransform)
    {
        Quaternion rotation = Quaternion.LookRotation(
        targetPos - myTransform.position,
        myTransform.TransformDirection(Vector3.up)
        );

        Quaternion rotation2D = new Quaternion(0, 0, rotation.z, rotation.w);
        return rotation2D;
    }
}

