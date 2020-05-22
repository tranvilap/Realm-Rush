﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DigitalRuby.SoundManagerNamespace;

namespace Game.Sound
{
    public class AudioManager
    {
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
    }
}