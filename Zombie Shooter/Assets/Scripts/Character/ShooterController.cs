using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Animations;

public class ShooterController : MonoBehaviour
{
    StarterAssetsInputs _input;
    ThirdPersonController _thirdPersonController;

    [SerializeField] CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] LayerMask aimColliderMask;
    [SerializeField] Transform aimVirtualTarget;

    [SerializeField] Transform weaponHolder;

    [SerializeField] Rig weaponSupportHandRig;
    [SerializeField] TwoBoneIKConstraint weaponSupportHandConstraint;

    [SerializeField] RigBuilder rigBuilder;

    Animator _animator;

    public Weapon currentWeapon;

    Vector3 _aimHitPoint = Vector3.zero;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Test();

        Aim();
        Shoot();
        HandleAimRaycast();
    }

    private void Aim()
    {
        if (_input.aim)
        {
            _thirdPersonController.SetRotateOnMove(false);

            aimVirtualCamera.gameObject.SetActive(true);

            Vector3 worldAimTarget = _aimHitPoint;
            aimVirtualTarget.position = _aimHitPoint;
            worldAimTarget.y = transform.position.y;

            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20);

            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1, Time.deltaTime * 15f));
        }
        else
        {
            _thirdPersonController.SetRotateOnMove(true);
            aimVirtualCamera.gameObject.SetActive(false);

            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0, Time.deltaTime * 15f));
        }
    }

    private void Shoot()
    {
        if (_input.shoot && _input.aim)
        {
            currentWeapon.FireProjectile(_aimHitPoint);
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

        //_animator.Rebind();
        rigBuilder.Build();

        currentWeapon = weapon;
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
    }
}
