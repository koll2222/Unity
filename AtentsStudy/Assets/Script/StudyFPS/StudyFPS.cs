using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudyFPS : MonoBehaviour
{
    public Slider bgmVolume;
    public Slider effectVolume;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(Resources.Load("Sound/BGM/Kingdom") as AudioClip);
        bgmVolume.value = SoundManager.Instance.BgmVolume;
        effectVolume.value = SoundManager.Instance.EffectVolume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnChangeBGMVolume(float v)
    {
        SoundManager.Instance.BgmVolume = v;
    }
    public void OnChangeEffectVolume(float v)
    {
        SoundManager.Instance.EffectVolume = v;
    }
}
