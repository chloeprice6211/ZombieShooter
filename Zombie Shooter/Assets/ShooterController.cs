using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;

public class ShooterController : MonoBehaviour
{
    StarterAssetsInputs _input;
    ThirdPersonController _thirdPersonController;

    [SerializeField] CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] LayerMask aimColliderMask;
    public Transform debugTransform;

    Vector3 _aimHitPoint = Vector3.zero;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    void Update()
    {


        Aim();
        HandleAimRaycast();
    }

    private void Aim()
    {
        if (_input.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);

            Vector3 worldAimTarget = _aimHitPoint;
            worldAimTarget.y = transform.position.y;

            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
        }
    }

    void HandleAimRaycast()
    {
        _aimHitPoint = Vector3.zero;

        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(center);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, aimColliderMask))
        {
            debugTransform.position = hit.point;
            _aimHitPoint = hit.point;
        }
    }
}
