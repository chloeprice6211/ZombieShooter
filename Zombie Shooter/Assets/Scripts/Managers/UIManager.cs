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
    [SerializeField] Image nvgStateFilledImg;
    [SerializeField] Image flashlightStateFilledImg;

    [SerializeField] Image weaponIconImage;
    [SerializeField] TextMeshProUGUI currentAmmo;
    [SerializeField] TextMeshProUGUI maxAmmo;

    [SerializeField] Image NVGIcon;
    [SerializeField] Image flashlightIcon;

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

    public void DisplayFilledImageState(float currentValue, FillImageCategory fillImageCategory, float maxValue)
    {
        Image fillImage = null;

        switch (fillImageCategory)
        {
            case FillImageCategory.Stamina:
                fillImage = staminaStateFilledImg;
                break;
            case FillImageCategory.NVG:
                fillImage = nvgStateFilledImg;
                break;
            case FillImageCategory.Flashlight:
                fillImage = flashlightStateFilledImg;
                break;
        }

        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, currentValue/maxValue, Time.deltaTime * 4);
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

    public void ChangeDeviceImage(FillImageCategory category, bool isActive)
    {
        switch (category)
        {
            case FillImageCategory.NVG:
                NVGIcon.color = isActive ? Color.green : Color.white;
                break;
            case FillImageCategory.Flashlight:
                flashlightIcon.color = isActive ? Color.green : Color.white;
                break;
        }
    }

}
public enum FillImageCategory
{
    Stamina,
    NVG,
    Flashlight
}
