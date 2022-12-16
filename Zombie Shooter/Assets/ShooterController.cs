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
        bool pressed = _input.aim ? true : false;
        aimVirtualCamera.gameObject.SetActive(pressed);
    }

    void HandleAimRaycast()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(center);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, aimColliderMask))
        {
            debugTransform.position = hit.point;
        }
    }
}
