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

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }
    void Update()
    {
        Aim();
    }

    private void Aim()
    {
        bool pressed = _input.aim ? true : false;
        aimVirtualCamera.gameObject.SetActive(pressed);
    }
}
