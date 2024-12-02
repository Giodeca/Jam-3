using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSetting : MonoBehaviour
{
    [SerializeField] Slider SFXSlider;

    [SerializeField] Slider MusicSlider;

    [SerializeField] Slider BrightnessSlider;

    private void Awake()
    {

        SFXSlider.value = Mathf.Pow(10, (DataManager.instance.SFXVolume - 0.01f) / 80);
        MusicSlider.value = Mathf.Pow(10, (DataManager.instance.MusicVolume - 0.01f) / 80);
        //Brightness.TryGet<ColorAdjustments>(out exposure);
        BrightnessSlider.value = (DataManager.instance.BrightnessValue + 2) / 4;
    }
    public void SfxSetVolume()
    {
        DataManager.instance.SFXMixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value + 0.01f) * 80);
        DataManager.instance.SFXMixer.GetFloat("SFXVolume", out DataManager.instance.SFXVolume);

    }
    public void MusicSetVolume()
    {
        DataManager.instance.MusicMixer.SetFloat("MusicVolume", Mathf.Log10(MusicSlider.value + 0.01f) * 80);
        DataManager.instance.MusicMixer.GetFloat("MusicVolume", out DataManager.instance.MusicVolume);
        Debug.Log(DataManager.instance.MusicVolume);
    }
    public void SetBrightness()
    {

        //Debug.Log(exposure.postExposure.value);
        DataManager.instance.exposure.postExposure.value = BrightnessSlider.value * 4 - 2;
        DataManager.instance.BrightnessValue = DataManager.instance.exposure.postExposure.value;
    }
}
