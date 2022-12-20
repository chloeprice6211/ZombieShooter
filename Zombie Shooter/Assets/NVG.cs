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
        Debug.Log("called");
        isOn = !isOn;

        if (isOn)
        {
            _animation.Play(activateNVGClip.name);
            volume.profile = nvgVolumeProfile;
            StartCoroutine(ChargeDecayRoutine(this));
        }
        else
        {
            _animation.Play(deactivateNVGClip.name);
            volume.profile = regularVolumeProfile;
            StartCoroutine(ChargeGainRoutine());
        }
    }
}
