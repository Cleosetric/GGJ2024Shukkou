using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Slider sliderBGM;
    public Slider sliderSFX;

    // Start is called before the first frame update
    void Start()
    {
        sliderBGM.onValueChanged.AddListener(OnBGMChange);
        sliderSFX.onValueChanged.AddListener(OnSFXChange);
        float savedBGMVolume = PlayerPrefs.GetFloat(
            "BGMVolumeKey",
            AudioManager.instance.defaultBGM
        );
        float savedSFXVolume = PlayerPrefs.GetFloat(
            "SFXVolumeKey",
            AudioManager.instance.defaultSFX
        );
        sliderBGM.value = savedBGMVolume;
        sliderSFX.value = savedSFXVolume;
    }

    void OnBGMChange(float volume)
    {
        AudioManager.Sound[] audioSound = AudioManager.instance.sounds;
        foreach (AudioManager.Sound sound in audioSound)
        {
            if (sound.audioType == AudioManager.Sound.AudioType.bgm)
            {
                sound.volume = volume;
                sound.source.volume = volume;
            }
        }
    }

    void OnSFXChange(float volume)
    {
        AudioManager.Sound[] audioSound = AudioManager.instance.sounds;
        foreach (AudioManager.Sound sound in audioSound)
        {
            if (sound.audioType == AudioManager.Sound.AudioType.sfx)
            {
                sound.volume = volume;
                sound.source.volume = volume;
            }
        }
    }

    public void OnSettingClose()
    {
        PlayerPrefs.SetFloat("BGMVolumeKey", sliderBGM.value);
        PlayerPrefs.SetFloat("SFXVolumeKey", sliderSFX.value);
        PlayerPrefs.Save();
    }
}
