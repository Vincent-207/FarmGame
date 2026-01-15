using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Rigidbody2D))]
public class RatLogic : MonoBehaviour
{
    public float moveSpeed;
    public Color color;
    Rigidbody2D _rigidbody2D;
    public float health;
    
    [Header("Attack settings")]
    [SerializeField]
    Barricade barricade;
    public float attackDamage, attackCooldown, currentAttackCooldown, attackRange;
    
    // public bool isNextToBarricade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        currentAttackCooldown = attackCooldown;
    }

    void FixedUpdate()
    {
        currentAttackCooldown -= Time.fixedDeltaTime;
        if(isNextToBarricade())
        {
            if(currentAttackCooldown <= 0)
            {
                Attack();
                currentAttackCooldown = attackCooldown;
            }
    
        }
        else
        {
            Move();
            
        }
        
    }

    void Attack()
    {
        barricade.DoDamage(attackDamage);
    }
    bool isNextToBarricade()
    {
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, transform.up, attackRange);
        foreach(RaycastHit2D raycastHit in raycastHits)
        {
            if(raycastHit.collider.CompareTag("Barricade"))
            {
                barricade = raycastHit.collider.GetComponent<Barricade>();
                return true;
            }
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        // Debug.Log("Collision!");
        if(collision2D.collider.CompareTag("Projectile"))
        {
            // Debug.Log("Hit!");
            IProjectile projectile = collision2D.collider.GetComponent<IProjectile>();
            health -= projectile.GetDamage();
            projectile.Destroy();

            if(health <= 0)
            {
                Die();
            }
        }
    }
    
    void Die()
    {
        Destroy(gameObject);
    }
    
    void TurnTowards()
    {
        
    }
    void Move()
    {
        _rigidbody2D.linearVelocity = transform.up * moveSpeed * Time.fixedDeltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        // Gizmos.DrawSphere(transform.position, detectionRadius);
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

