using Game.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectSFX : MonoBehaviour
{
    [SerializeField] SFXObj sfx = null;

    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        AudioManager.PlayOneShotSound(audioSource, sfx);
    }
}
