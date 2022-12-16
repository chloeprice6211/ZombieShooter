using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public BulletProjectile bullet;
    public Transform firePosition;

    public void FireProjectile(Vector3 targetPosition)
    {
        Vector3 target = (targetPosition - firePosition.position).normalized;
        Instantiate(bullet, firePosition.position, Quaternion.LookRotation(target, Vector3.up));
    }
}
