using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public Transform bulletParent;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int currentAmmo, magCapacity;
    [SerializeField] InputActionReference shoot, reload;
    [SerializeField] TMP_Text ammoCounter;
    [SerializeField] float reloadDuration, spawnDistance;
    [SerializeField] bool isReloading;

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
            Debug.Log("Can't shoot, out of ammo. Starting reload.");
            TryStartReload();
            return;
        }
        else if(isReloading)
        {
            
            Debug.Log("Can't shoot, currently reloading.");
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
        GameObject shotBullet = Instantiate(bulletPrefab, spawnPos, Quaternion.Euler(0f, 0f, rot_z - 90), bulletParent);

        currentAmmo--;
        UpdateAmmoCounter();
    }

    void TryStartReload(InputAction.CallbackContext context)
    {
        TryStartReload();
    }

    void TryStartReload()
    {
        
        if(isReloading == false && currentAmmo < magCapacity)
        {
            Debug.Log("Reloading!");
            StartCoroutine(Reload());
            
        }
        else
        {
            Debug.LogWarning("Can't start reload, already reloading");
        }

        
    }
    IEnumerator Reload()
    {
        float reloadTime = reloadDuration;
        isReloading = true;
        while(reloadTime > 0)
        {
            ammoCounter.text = "Reloading";
            reloadTime -= Time.deltaTime;
            yield return null;
        }

        currentAmmo = magCapacity;
        UpdateAmmoCounter();
        isReloading = false;
    }

    void UpdateAmmoCounter()
    {
        ammoCounter.text = String.Format("{0} / {1}", currentAmmo, magCapacity);
    }
}
