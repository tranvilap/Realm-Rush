using Game.Sound;
using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeSlider : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat(Constants.GLOBAL_SFX, 1f);
    }

    public void ChangeMusicVolume(float volume)
    {
        AudioManager.ChangeGlobalSFXVolume(volume);
    }
}
