using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootGunTest : MonoBehaviour
{
    [SerializeField]
    InputActionReference shootReference;
    [SerializeField]
    GameObject bullet;
    public float spawnDistance;
    private void OnEnable()
    {
        shootReference.action.started += Shoot;
    }

    private void OnDisable()
    {
        shootReference.action.started -= Shoot;
        
    }

    void Shoot(InputAction.CallbackContext obj)
    {
        // Get mouse direction
        Vector3 toMouse = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position).normalized;
        Vector3 debugPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        debugPos.z = 0;
        toMouse.z = 0;
        toMouse.Normalize();
        Vector3 spawnPos = (toMouse * spawnDistance) + transform.position;
        spawnPos.z = 0;
        float rot_z = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;
        GameObject shotBullet = Instantiate(bullet, spawnPos, Quaternion.Euler(0f, 0f, rot_z - 90));
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Quaternion LookAt2D(Transform target)
    {
        Quaternion rotation = Quaternion.LookRotation(
        target.transform.position - transform.position,
        transform.TransformDirection(Vector3.up)
        );

        Quaternion rotation2D = new Quaternion(0, 0, rotation.z, rotation.w);
        return rotation2D;
    }
}
