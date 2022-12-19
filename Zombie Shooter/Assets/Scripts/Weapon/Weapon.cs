using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Weapon : MonoBehaviour
{
    // components
    public BulletProjectile bullet;
    public Transform firePosition;
    public Transform supportHandPos;
    public Transform muzzleFlash;
    public Sprite weaponIcon;

    // mag animation
    [Space]
    public GameObject magazine;
    public Animation magReloadAnimation;
    public Transform magHolder;
    [SerializeField] GameObject currentMag;

    // stats
    [Space]
    public string weaponName;
    public float ammoCapacity;
    public float fireRate = .5f;
    public float currentAmmo;

    //audio
    [SerializeField] AudioSource shootAudioSource;
    [SerializeField] AudioSource reloadAudioSource;

    [SerializeField] AudioClip shootSound;

    public bool isReloading;

    bool _canShoot = true;
    float _timeElapsed;

    Rig _weaponSupportRig;

    [SerializeField] AnimationClip insertMagClip; 


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
        UIManager.Instance.UpdateWeaponCurrentAmmo(currentAmmo);

        shootAudioSource.PlayOneShot(shootSound);

        _timeElapsed = 0;
        _canShoot = false;

        Vector3 target = (targetPosition - firePosition.position).normalized;
        Instantiate(bullet, firePosition.position, Quaternion.LookRotation(target, Vector3.up));
        Instantiate(muzzleFlash, firePosition.position, Quaternion.LookRotation(target, Vector3.up), firePosition);
    }

    public void Reload(Rig handRig)
    {
        reloadAudioSource.Play();
        _weaponSupportRig = handRig;
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        isReloading = true;

        while (_weaponSupportRig.weight > 0)
        {
            _weaponSupportRig.weight -= Time.deltaTime * 5;
            yield return null;
        }

        currentMag.GetComponent<Animation>().Play("MagazineReload");

        yield return new WaitForSeconds(.5f);
        currentMag.transform.SetParent(null);

        yield return new WaitForSeconds(1f);
        GameObject newMag = Instantiate(magazine, magHolder);
        newMag.transform.localPosition = Vector3.zero;
        newMag.transform.localRotation = Quaternion.identity;
        magReloadAnimation = newMag.GetComponent<Animation>();
        magReloadAnimation.Play(insertMagClip.name);

        currentMag = newMag;
        
        yield return new WaitForSeconds(1.2f);

        currentAmmo = ammoCapacity;
        
        UIManager.Instance.UpdateWeaponCurrentAmmo(currentAmmo);

        isReloading = false;

        while(_weaponSupportRig.weight < 1)
        {
            _weaponSupportRig.weight += Time.deltaTime * 5;
            yield return null;
        } 
    }

}
