using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public BulletProjectile bullet;
    public Transform firePosition;

    public float ammoCapacity;
    public float fireRate = .5f;

    public float currentAmmo;

    public Transform supportHandPos;
    public Transform muzzleFlash;

    public bool isReloading;

    bool _canShoot = true;
    float _timeElapsed;

    private void Start()
    {
        currentAmmo = ammoCapacity;
    }

    private void Update()
    {
        if (!_canShoot)
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= fireRate)
            {
                _canShoot = true;
            }
        }
    }

    public void FireProjectile(Vector3 targetPosition)
    {
        if (!_canShoot) return;
        if (currentAmmo < 1) return;

        currentAmmo--;

        _timeElapsed = 0;
        _canShoot = false;

        Vector3 target = (targetPosition - firePosition.position).normalized;
        Instantiate(bullet, firePosition.position, Quaternion.LookRotation(target, Vector3.up));
        Instantiate(muzzleFlash, firePosition.position, Quaternion.LookRotation(target, Vector3.up), firePosition);
    }

    public void Reload()
    {
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        Debug.Log("reloading");

        isReloading = true;
        yield return new WaitForSeconds(2f);
        currentAmmo = ammoCapacity;
        isReloading = false;

        Debug.Log(currentAmmo);
    }

}
