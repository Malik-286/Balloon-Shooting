using UnityEngine;
using System.Collections;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

/// <summary>
/// Game effect script.
/// </summary>
public class GameEffect : MonoBehaviour
{
		/// <summary>
		/// Hide the effect.
		/// </summary>
		public void Hide ()
		{
				//Hide the effect using animator
				GetComponent<Animator> ().SetBool ("Show", false);
		}
}