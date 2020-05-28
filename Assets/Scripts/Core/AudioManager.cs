using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DigitalRuby.SoundManagerNamespace;

namespace Game.Sound
{
    public class AudioManager
    {
        public static float GlobalMusicVolume
        {
            get
            {
                return SoundManager.MusicVolume;
            }
        }
        public static float GlobalSFXVolume
        {
            get
            {
                return SoundManager.SoundVolume;
            }
        }
        public static void ChangeGlobalMusicVolume(float volume)
        {
            SoundManager.MusicVolume = volume;
            PlayerPrefs.SetFloat(Constants.GLOBAL_MUSIC, volume);
        }
        public static void ChangeGlobalSFXVolume(float volume)
        {
            SoundManager.SoundVolume = volume;
            PlayerPrefs.SetFloat(Constants.GLOBAL_SFX, volume);
        }

        public static void PlayOneShotSound(AudioSource audioSource, SFXObj sfx)
        {
            if (audioSource != null && sfx != null)
            {
                if (sfx.clip != null)
                {
                    audioSource.PlayOneShotSoundManaged(sfx.clip, sfx.volume);
                }
            }
        }
        public static void PlayOneShotSound(AudioSource audioSource, SFX sfx)
        {
            if (audioSource != null && sfx.clip != null)
            {
                audioSource.PlayOneShotSoundManaged(sfx.clip, sfx.volume);
            }
        }

        public static void PlayOneShotMusic(AudioSource audioSource, MusicObj musicObj)
        {
            if (musicObj == null || audioSource == null) { return; }
            if (musicObj.clip == null) { return; }
            audioSource.PlayOneShotMusicManaged(musicObj.clip, musicObj.volume);
        }
        public static void PlayLoopMusic(AudioSource audioSource, MusicObj musicObj, bool persist)
        {
            if (musicObj == null || audioSource == null) { return; }
            if (musicObj.clip == null) { return; }
            audioSource.clip = musicObj.clip;
            audioSource.pitch = musicObj.pitch;
            audioSource.PlayLoopingMusicManaged(musicObj.volume, musicObj.fadeTime, persist);
        }

        public static void UpdateVolumes()
        {
            ChangeGlobalMusicVolume(PlayerPrefs.GetFloat(Constants.GLOBAL_MUSIC, 1f));
            ChangeGlobalSFXVolume(PlayerPrefs.GetFloat(Constants.GLOBAL_SFX, 1f));
        }
    }
}