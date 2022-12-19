using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;

    [SerializeField] Image staminaStateFilledImg;

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void DisplayStaminaState(float currentStaminaState)
    {
        staminaStateFilledImg.fillAmount = currentStaminaState / 100;
    }
}
