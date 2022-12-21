using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HelmetDevice : MonoBehaviour
{
    public bool isOn;
    public float batteryCharge = 100f;
    public float decayRate = 1f;
    public float minimalCharge = 15f;
    public FillImageCategory category;

    public abstract void ActivateOrDeactivateDevice();

    protected IEnumerator ChargeDecayRoutine(HelmetDevice device)
    {
        while (isOn)
        {
            batteryCharge -= Time.deltaTime * decayRate;
            UIManager.Instance.DisplayFilledImageState(batteryCharge, device.category, 100);

            if(batteryCharge <= 0f)
            {
                device.ActivateOrDeactivateDevice();
            }

            yield return null;
        }
    }
    protected IEnumerator ChargeGainRoutine(HelmetDevice device)
    {
        while(!isOn && batteryCharge <= 100)
        {
            UIManager.Instance.DisplayFilledImageState(batteryCharge, device.category, 100);
            batteryCharge += Time.deltaTime * decayRate * 3;
            yield return null;
        }
    }
}
