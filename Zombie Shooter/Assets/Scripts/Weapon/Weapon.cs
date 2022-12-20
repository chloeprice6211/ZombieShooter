using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Weapon : MonoBehaviour
{
    [Header("components")]
    public BulletProjectile bullet;
    public Transform firePosition;
    public Transform supportHandPos;
    public Transform muzzleFlash;
    public Sprite weaponIcon;

    [Header("mag information")]
    public WeaponMag magazine;
    public Animation magReloadAnimation;
    public Transform magHolder;
    [SerializeField] WeaponMag currentMag;
    [SerializeField] AnimationClip insertMagClip;
    public bool isReloading;

    [Header("weapon stats")]
    public string weaponName;
    public float ammoCapacity;
    public float fireRate = .5f;
    public float currentAmmo;

    [Header("audio")]
    [SerializeField] AudioSource shootAudioSource;
    [SerializeField] AudioSource reloadAudioSource;
    [SerializeField] AudioClip shootSound;

    bool _canShoot = true;
    float _timeElapsed;


    private void Start()
    {
        currentAmmo = magazine.ammoCapacity;
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


    public void Reload()
    {
        reloadAudioSource.Play();
        StartCoroutine(ReloadRoutine());
    }
    IEnumerator ReloadRoutine()
    {
        isReloading = true;

        currentMag.GetComponent<Animation>().Play("MagazineReload");
        currentMag.DropMag();

        yield return new WaitForSeconds(.5f);
        currentMag.transform.SetParent(null);

        yield return new WaitForSeconds(1f);
        WeaponMag newMag = Instantiate(magazine, Vector3.zero, Quaternion.identity, magHolder);
        magReloadAnimation = newMag.GetComponent<Animation>();
        magReloadAnimation.Play(insertMagClip.name);

        currentMag = newMag;
        
        yield return new WaitForSeconds(1.2f);

        currentAmmo = magazine.ammoCapacity;
        isReloading = false;

        UIManager.Instance.UpdateWeaponCurrentAmmo(currentAmmo);

    }

}
