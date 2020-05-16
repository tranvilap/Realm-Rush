using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Sound
{
    [Serializable]
    public class SFX
    {
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }
}
