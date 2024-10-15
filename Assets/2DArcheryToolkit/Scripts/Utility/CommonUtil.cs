using UnityEngine;
using System.Collections;
using System;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

public class CommonUtil
{
		/// <summary>
		/// Converts bool value true/false to int value 0/1.
		/// </summary>
		/// <returns>The int value.</returns>
		/// <param name="value">The bool value.</param>
		public static int TrueFalseBoolToZeroOne (bool value)
		{
				if (value) {
						return 1;
				}
				return 0;
		}

		/// <summary>
		/// Converts int value 0/1 to bool value true/false.
		/// </summary>
		/// <returns>The bool value.</returns>
		/// <param name="value">The int value.</param>
		public static bool ZeroOneToTrueFalseBool (int value)
		{
				if (value == 1) {
						return true;
				} else {
						return false;
				}
		}

		/// <summary>
		/// Play the one shot clip.
		/// </summary>
		/// <param name="audioClip">Audio clip.</param>
		/// <param name="postion">Postion.</param>
		/// <param name="volume">Volume.</param>
		public static void PlayOneShotClipAt (AudioClip audioClip,Vector3 postion,float volume){

				if (audioClip == null || volume == 0) {
						return;
				}

				GameObject oneShotAudio = new GameObject("one shot audio"); 
				oneShotAudio.transform.position = postion; 

				AudioSource tempAudioSource = oneShotAudio.AddComponent<AudioSource>(); //add an audio source
				tempAudioSource.clip = audioClip;//set the audio clip
				tempAudioSource.volume = volume;//set the volume
				tempAudioSource.loop = false;//set loop to false
				tempAudioSource.rolloffMode = AudioRolloffMode.Linear;//linear rolloff mode
				tempAudioSource.Play();// play audio clip
				GameObject.Destroy(oneShotAudio,audioClip.length); //destroy oneShotAudio gameobject after clip duration
		}
}
