using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource bgmPlayer = null;
    float _effectVolume = 1.0f;
    public float BgmVolume
    {
        get => bgmPlayer.volume;
        set
        {
            PlayerPrefs.SetFloat("BGM_Volume", 1f - value);  // PlayerPrefs : int, float, string, bool 가능, 중요하지 않은 데이터만 저장
            bgmPlayer.volume = value;
        }
    }
    public float EffectVolume
    {
        get => _effectVolume;
        set
        {
            PlayerPrefs.SetFloat("Effect_Volume", 1f - value);
            _effectVolume = value;
        }
    }
    private void Awake()
    {
        base.Initialize();
        if (bgmPlayer == null)
        {
            bgmPlayer = Camera.main.GetComponent<AudioSource>();
            if (bgmPlayer == null)
            {
                bgmPlayer = Camera.main.AddComponent<AudioSource>();
            }
        }
        bgmPlayer.volume = 1f - PlayerPrefs.GetFloat("BGM_Volume");
        _effectVolume = 1f - PlayerPrefs.GetFloat("Effect_Volume");
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        bgmPlayer.clip = clip;
        bgmPlayer.loop = loop;
        bgmPlayer.Play();
    }

    public void PauseBGM()
    {
        bgmPlayer.Pause();
    }
    public void ResumeBGM()
    {
        bgmPlayer.Play();
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlayEffectSound(AudioSource audio, AudioClip clip)
    {
        if (EffectVolume > 0f)
        {
            audio.volume = EffectVolume;
            audio.PlayOneShot(clip);
        }
    }
}
