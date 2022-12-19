using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;

    [SerializeField] Image staminaStateFilledImg;

    [SerializeField] Image weaponIconImage;
    [SerializeField] TextMeshProUGUI currentAmmo;
    [SerializeField] TextMeshProUGUI maxAmmo;

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

    public void SetWeaponUI(Weapon weapon)
    {
        weaponIconImage.sprite = weapon.weaponIcon;
        currentAmmo.text = weapon.currentAmmo.ToString();
        maxAmmo.text = weapon.ammoCapacity.ToString();
    }

    public void UpdateWeaponCurrentAmmo(float amount)
    {
        currentAmmo.text = amount.ToString();
    }
}
