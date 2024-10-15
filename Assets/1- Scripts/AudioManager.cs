using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

 
    public AudioSource audioSource;


    [SerializeField] AudioClip touchSFX;
    [SerializeField] AudioClip balloonPopupSFX;
     


    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();

    }

    

    public void PlaySingleShotAudio(AudioClip audioClip, float soundEffect)
    {
        audioSource.PlayOneShot(audioClip,soundEffect);
    }

    public void PlayTouchSoundEffect()
    {
        audioSource.PlayOneShot(touchSFX, 0.8f);
    }

    public void PlayBalloonPopupSoundWEffect()
    {
        audioSource.PlayOneShot(balloonPopupSFX, 1.3f);
    }
}
