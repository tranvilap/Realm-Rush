using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Sound
{
    [CreateAssetMenu(menuName = "Audio/Music")]
    public class MusicObj : ScriptableObject
    {
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        public float pitch = 1f;
        public float fadeTime = 0f;
    }
}
