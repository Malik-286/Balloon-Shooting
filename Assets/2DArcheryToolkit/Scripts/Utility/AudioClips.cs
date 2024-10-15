using UnityEngine;
using System.Collections;

///Developed By Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

public class AudioClips : MonoBehaviour
{
		/// <summary>
		/// A static instance of this class.
		/// </summary>
		public static AudioClips instance;

		//AudioClips references
		public AudioClip backgroundMusic;
		public AudioClip bubblePopSFX;
		public AudioClip arrowImpactSFX;
		public AudioClip arrowSwooshSFX;

		void Awake ()
		{
				if (instance == null) {
						instance = this;
						DontDestroyOnLoad (gameObject);
				} else {
						Destroy (gameObject);
				}
		}

		void Start ()
		{
				//Play the background music clip on start
				PlayBackgroundMusic ();
		}

		public void PlayBackgroundMusic ()
		{
				AudioSources.instance.MusicAudioSource ().clip = backgroundMusic;
				AudioSources.instance.MusicAudioSource ().Play ();
		}

		public void PlayBubblePopSFX ()
		{
				AudioSources.instance.PlaySFXClip (bubblePopSFX, false);
		}

		public void PlayArrowImpactSFX ()
		{
				AudioSources.instance.PlaySFXClip (arrowImpactSFX, false);
		}

		public void PlayArrowSwooshSFX ()
		{
				AudioSources.instance.PlaySFXClip (arrowSwooshSFX, false);
		}
}
