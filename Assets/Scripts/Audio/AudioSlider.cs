using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    Slider slider;

    public enum SoundType
    {
        Master,
        Music,
        SFX
    }

    public SoundType type;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChange);
        OnValueChange(slider.value);
    }

    void OnValueChange(float value)
    {
        float newValue = 15.8125f * Mathf.Sqrt(value) - 50.0f;

        switch (type)
        {
            case SoundType.Master:
                AudioManager.instance.SetMasterVolume(newValue);
                break;
            case SoundType.Music:
                AudioManager.instance.SetMusicVolume(newValue);
                break;
            case SoundType.SFX:
                AudioManager.instance.SetSFXVolume(newValue);
                break;
        }
    }
}
