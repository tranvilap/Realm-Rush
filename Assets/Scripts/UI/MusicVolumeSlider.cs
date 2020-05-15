using Game.Sound;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat(Constants.GLOBAL_MUSIC,1f);
    }

    public void ChangeMusicVolume(float volume)
    {
        AudioManager.ChangeGlobalMusicVolume(volume);
    }
}
