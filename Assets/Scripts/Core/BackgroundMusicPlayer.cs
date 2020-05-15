using DigitalRuby.SoundManagerNamespace;
using Game.Sound;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] MusicObj bgm = null;
    AudioSource bgmAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        if (bgm != null)
        {
            bgmAudioSource.clip = bgm.clip;
            bgmAudioSource.pitch = bgm.pitch;

            bgmAudioSource.PlayLoopingMusicManaged(bgm.volume, bgm.fadeTime);
        }
    }
}
