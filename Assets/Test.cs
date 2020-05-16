using Game.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.SoundManagerNamespace;

public class Test : MonoBehaviour
{
    [SerializeField] SFXObj[] sfx;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.PlayLoopingMusicManaged(1, 2f, false);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            AudioManager.PlayOneShotSound(audioSource, sfx[0]);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            AudioManager.PlayOneShotSound(audioSource, sfx[1]);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            AudioManager.PlayOneShotSound(audioSource, sfx[2]);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            AudioManager.PlayOneShotSound(audioSource, sfx[3]);
        }
    }
}
