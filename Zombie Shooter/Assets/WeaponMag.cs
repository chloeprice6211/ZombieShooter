using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMag : MonoBehaviour
{
    public float ammoCapacity;


    public void DropMag()
    {
        StartCoroutine(OnMagDropRoutine());
    }
    IEnumerator OnMagDropRoutine()
    {
        yield return new WaitForSeconds(3f);
        gameObject.layer = 0;

        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }

}
