using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Sound
{
    [CreateAssetMenu(menuName = "Audio/SFX")]
    public class SFXObj : ScriptableObject
    {
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }
}