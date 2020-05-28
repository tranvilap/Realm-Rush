using Game.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.SoundManagerNamespace;
public class MainMenuController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] LevelLoader levelLoader = null;
    [SerializeField] AudioSource musicPlayer = null;
    [SerializeField] MusicObj bgm = null;
    [SerializeField] AudioClip buttonSound = null;


    [Header("Setting")]
    [SerializeField] GameObject settingPanel = null;
    [SerializeField] Slider musicSlider = null;
    [SerializeField] Slider sfxSlider = null;

    private void Start()
    {
        AudioManager.UpdateVolumes();
        AudioManager.PlayLoopMusic(musicPlayer, bgm, false);
    }

    public void OnClickSettingButton()
    {
        PlayButtonSound();
        settingPanel.SetActive(true);
        musicSlider.value = PlayerPrefs.GetFloat(Constants.GLOBAL_MUSIC, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(Constants.GLOBAL_SFX, 1f);
    }
    public void OnClickPlayButton()
    {
        PlayButtonSound();
        levelLoader.LoadSceneDelay(1, buttonSound.length);
    }
    public void PlayButtonSound()
    {
        musicPlayer.PlayOneShotSoundManaged(buttonSound);
    }
    public void OnMusicVolumeChange(float volume)
    {
        AudioManager.ChangeGlobalMusicVolume(volume);
    }
    public void OnSFXVolumeChange(float volume)
    {
        AudioManager.ChangeGlobalSFXVolume(volume);
    }
}
