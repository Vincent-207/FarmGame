using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class RatLogic : MonoBehaviour
{
    public float moveSpeed, detectionRadius, thresholdDistance;
    bool isRunning = false;
    Vector3 lastSeenScare;
    Vector3 moveDir;
    Coroutine wanderRoutine, runRoutine;
    public Color color;
    public bool debugBool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            moveDir =  Random.insideUnitCircle;
            Move();
            yield return null;
        }
    }
    void Move()
    {
        transform.position = transform.position + (moveDir * moveSpeed);
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
}

