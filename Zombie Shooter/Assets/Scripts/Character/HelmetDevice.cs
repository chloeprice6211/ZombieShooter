using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetDevice : MonoBehaviour
{
    public bool isOn;

    public virtual void ActivateOrDeactivateDevice()
    {
        isOn = !isOn;
    }
}
