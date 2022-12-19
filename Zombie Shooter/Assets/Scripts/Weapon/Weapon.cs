using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public BulletProjectile bullet;
    public Transform firePosition;

    public float fireRate = .5f;

    public Transform supportHandPos;

    bool _canShoot = true;
    float _timeElapsed;

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

        _timeElapsed = 0;
        _canShoot = false;

        Vector3 target = (targetPosition - firePosition.position).normalized;
        Instantiate(bullet, firePosition.position, Quaternion.LookRotation(target, Vector3.up));
    }

}
