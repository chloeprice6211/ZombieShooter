using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NVG : HelmetDevice
{
    public Transform lens;
    Animation _animation;

    [SerializeField] AnimationClip activateNVGClip;
    [SerializeField] AnimationClip deactivateNVGClip;

    [SerializeField] Volume volume;

    [SerializeField] VolumeProfile regularVolumeProfile;
    [SerializeField] VolumeProfile nvgVolumeProfile;

    private void Awake()
    {
        _animation = lens.GetComponent<Animation>();
    }

    public override void ActivateOrDeactivateDevice()
    {
        if (!isOn && batteryCharge <= minimalCharge) return;

        isOn = !isOn;

        if (isOn)
        {
            UIManager.Instance.ChangeDeviceImage(category, true);
            _animation.Play(activateNVGClip.name);
            volume.profile = nvgVolumeProfile;
            StartCoroutine(ChargeDecayRoutine(this));
        }
        else
        {
            UIManager.Instance.ChangeDeviceImage(category, false);
            _animation.Play(deactivateNVGClip.name);
            volume.profile = regularVolumeProfile;
            StartCoroutine(ChargeGainRoutine(this));
        }
    }
}
