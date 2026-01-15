using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    GameObject bulletPrefab;
    int currentAmmo, magCapacity;
    InputActionReference shoot, reload;
    TMP_Text ammoCounter;
    float reloadDuration, spawnDistance;
    Coroutine reloadRoutine;

    void OnEnable()
    {
        shoot.action.started += TryShoot;
        reload.action.started += TryStartReload;
    }

    void OnDisable()
    {
        shoot.action.started -= TryShoot;
        reload.action.started -= TryStartReload;
    }

    void TryShoot(InputAction.CallbackContext context)
    {
        
        if(currentAmmo <= 0)
        {
            TryStartReload();
            return;
        }
        else
        {
            Shoot();
        }

    }

    void Shoot()
    {
        Vector3 toMouse = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position).normalized;
        toMouse.z = 0;
        toMouse.Normalize();
        Vector3 spawnPos = (toMouse * spawnDistance) + transform.position;
        spawnPos.z = 0;
        float rot_z = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;
        GameObject shotBullet = Instantiate(bulletPrefab, spawnPos, Quaternion.Euler(0f, 0f, rot_z - 90));

        currentAmmo--;
        UpdateAmmoCounter();
    }

    void TryStartReload(InputAction.CallbackContext context)
    {
        TryStartReload();
    }

    void TryStartReload()
    {
        
        if(reloadRoutine == null)
        {
            reloadRoutine = StartCoroutine(ReloadRoutine());
            
        }
        else
        {
            Debug.LogWarning("Can't start reload, already reloading");
        }

        
    }
    IEnumerator ReloadRoutine()
    {
        float reloadTime = reloadDuration;

        while(reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
            yield return null;
        }

        currentAmmo = magCapacity;
        UpdateAmmoCounter();
    }

    void UpdateAmmoCounter()
    {
        ammoCounter.text = String.Format("{0} / {0}", currentAmmo, magCapacity);
    }
}
