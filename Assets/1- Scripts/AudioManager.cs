using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

 
    public AudioSource audioSource;


    [SerializeField] AudioClip touchSFX;
    [SerializeField] AudioClip balloonPopupSFX;

    [SerializeField] AudioClip gernadeSoundEffect;


    [SerializeField] AudioClip winSoundEffect;
    [SerializeField] AudioClip loseSoundEffect;



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
        audioSource.PlayOneShot(balloonPopupSFX, 1.2f);
    }

    public void PlayWinSoundEffect()
    {
        audioSource.PlayOneShot(winSoundEffect, 1.3f);
     }

    public void PlayLoseSoundEffect()
    {
        audioSource.PlayOneShot(loseSoundEffect, 1.3f);
 
    }

    public void PlayGernadeSoundEffect()
    {
        audioSource.PlayOneShot(gernadeSoundEffect, 1.5f);
 
    }
}
