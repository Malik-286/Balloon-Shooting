using UnityEngine;
using System.Collections;

///Developed By Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

public class AudioSources : MonoBehaviour
{
	/// <summary>
	/// This Gameobject defined as a Singleton.
	/// </summary>
	public static AudioSources instance;

	/// <summary>
	/// The audio sources references.
	/// First Audio Souce used for the music
	/// Second Audio Souce used for the sound effects
	/// </summary>
	private AudioSource[] audioSources;

	/// <summary>
	/// The music audio source key.
	/// </summary>
	public static string musicAudioSourceVolumeKey = "MusicAudioSourceMute";

	/// <summary>
	/// The sfx audio source key.
	/// </summary>
	public static string sfxAudioSourceVolumeKey = "SFXAudioSourceMute";

	void Awake ()
	{
		if (instance == null) {
			instance = this;
			audioSources = GetComponents<AudioSource> ();
			SetUpVolumeValues ();
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}


	/// <summary>
	/// Set up the volume values for sfx, music audio sources.
	/// </summary>
	private void SetUpVolumeValues ()
	{
		if (PlayerPrefs.HasKey (musicAudioSourceVolumeKey)) {
			MusicAudioSource ().volume = PlayerPrefs.GetFloat (musicAudioSourceVolumeKey);
		}

		if (PlayerPrefs.HasKey (sfxAudioSourceVolumeKey)) {
			SFXAudioSource ().volume = PlayerPrefs.GetFloat (sfxAudioSourceVolumeKey);
		}
	}

	/// <summary>
	/// Play the given SFX clip.
	/// </summary>
	/// <param name="clip">The Clip reference.</param>
	/// <param name="Stop Current Clip">If set to <c>true</c> stop current clip.</param>
	public void PlaySFXClip (AudioClip clip, bool stopCurrentClip)
	{
		if (clip == null) {
			return;
		}
		if (stopCurrentClip) {
			SFXAudioSource ().Stop ();
		}
		SFXAudioSource ().PlayOneShot (clip);	
	}

	/// <summary>
	/// Returns the Audio Source of the Music.
	/// </summary>
	/// <returns>The Audio Source of the Music.</returns>
	public AudioSource MusicAudioSource ()
	{
		return audioSources [0];
	}

	/// <summary>
	/// Returns the Audio Source of the Sound Effects.
	/// </summary>
	/// <returns>The Audio Source of the Sound Effects.</returns>
	public AudioSource SFXAudioSource ()
	{
		return audioSources [1];
	}
}

