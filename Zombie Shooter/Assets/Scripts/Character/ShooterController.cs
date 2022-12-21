using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Animations;

public class ShooterController : MonoBehaviour, IDamagable
{
    // controllers
    StarterAssetsInputs _input;
    ThirdPersonController _thirdPersonController;

    [Header("aim components")]
    [SerializeField] CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] LayerMask aimColliderMask;
    [SerializeField] Transform aimVirtualTarget;

    [Header("inverse kinematics")]
    [SerializeField] TwoBoneIKConstraint weaponSupportHandConstraint;
    [SerializeField] RigBuilder rigBuilder;
    [SerializeField] Rig aimRig;
    [SerializeField] Rig weaponSupportHandRig;
    [SerializeField] Transform weaponHolder;

    [Header("helmet devices")]
    [SerializeField] TacticalFlashlight flashlight;
    [SerializeField] NVG nvg;

    // animator related
    int _reloadHash;
    Animator _animator;

    [Header("misc")]
    public float healthPoints;
    public Weapon currentWeapon;
    Vector3 _aimHitPoint = Vector3.zero;


    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();

        _reloadHash = Animator.StringToHash("Reload");

    }

    private void Start()
    {
        SetWeapon(GameManager.Instance.Weapons[0]);
    }

    void Update()
    {
        Test();
        Reload();
        Aim();
        Shoot();
        HandleAimRaycast();
        HandleHelmetDevice();
    }

    private void Aim()
    {
        if (_input.aim)
        {
            _thirdPersonController.SetRotateOnMove(false);

            aimVirtualCamera.gameObject.SetActive(true);

            Vector3 worldAimTarget = _aimHitPoint;
            aimVirtualTarget.position = Vector3.Lerp(aimVirtualTarget.position, _aimHitPoint, Time.deltaTime * 10);
            worldAimTarget.y = transform.position.y;

            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20);

            if (!currentWeapon.isReloading)
            {
                aimRig.weight = Mathf.Lerp(aimRig.weight, 1, Time.deltaTime * 3);
            }

            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1, Time.deltaTime * 15f));
        }
        else
        {
            _thirdPersonController.SetRotateOnMove(true);
            aimVirtualCamera.gameObject.SetActive(false);

            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0, Time.deltaTime * 15f));

            if(aimRig.weight > 0)
            {
                aimRig.weight = Mathf.Lerp(aimRig.weight, 0, Time.deltaTime * 3);
            }
          
        }
    }
    private void Shoot()
    {
        if (_input.shoot && _input.aim && !currentWeapon.isReloading)
        {
            currentWeapon.FireProjectile(_aimHitPoint);
        }
    }
    private void Reload()
    {
        if (_input.reload && !currentWeapon.isReloading)
        {
            if (currentWeapon.currentAmmo == currentWeapon.ammoCapacity) return;

            StartCoroutine(SmoothReloadRoutine());
            _animator.Play(_reloadHash, 2);
            currentWeapon.Reload();
        }

        IEnumerator SmoothReloadRoutine()
        {
            float value = 0f;

            _animator.SetLayerWeight(2, 0);

            while (_animator.GetLayerWeight(2) < 1)
            {
                value += Time.deltaTime * 3f;

                _animator.SetLayerWeight(2, value);

                aimRig.weight -= Time.deltaTime * 3f;
                weaponSupportHandRig.weight -= Time.deltaTime * 3f;

                yield return null;
            }

            yield return new WaitForSeconds(2.5f);

            while (_animator.GetLayerWeight(2) > 0)
            {
                value -= Time.deltaTime * 3f;
                _animator.SetLayerWeight(2, value);

                if (_input.aim)
                {
                    aimRig.weight += Time.deltaTime * 3f;
                }
                
                weaponSupportHandRig.weight += Time.deltaTime * 3f;

                yield return null;
            }

        }
    }
    private void HandleHelmetDevice()
    {
        if (_input.flashlight)
        {
            flashlight.ActivateOrDeactivateDevice();
            _input.flashlight = false;
        }

        if (_input.nvg)
        {
            nvg.ActivateOrDeactivateDevice();
            _input.nvg = false;
        }
    }


    void HandleAimRaycast()
    {
        _aimHitPoint = Vector3.zero;

        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(center);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, aimColliderMask))
        {
            _aimHitPoint = hit.point;
        }
    }

    void SetWeapon(Weapon weapon)
    {
        _animator.enabled = false;
        weaponSupportHandRig.weight = 0;

        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        weapon = Instantiate(weapon, weaponHolder);

        AnimatorJobExtensions.UnbindAllStreamHandles(_animator);
        AnimatorJobExtensions.UnbindAllSceneHandles(_animator);

        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);

        weaponSupportHandConstraint.data.target = weapon.supportHandPos;

        rigBuilder.Build();

        currentWeapon = weapon;
        UIManager.Instance.SetWeaponUI(currentWeapon);

        weaponSupportHandRig.weight = 1;

        _animator.enabled = true;

    }

    void Test()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(GameManager.Instance.Weapons[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(GameManager.Instance.Weapons[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeapon(GameManager.Instance.Weapons[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetWeapon(GameManager.Instance.Weapons[3]);
        }

    }

    public void TakeDamage(float damage)
    {
        if(healthPoints - damage < 1)
        {
            OnDeath();
        }
        healthPoints -= damage;
    }

    public void OnDeath()
    {
        Debug.Log("dead");
    }
}
