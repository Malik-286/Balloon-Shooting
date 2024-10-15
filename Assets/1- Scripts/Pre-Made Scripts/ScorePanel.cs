using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com
 
/// <summary>
/// Score panel script.
/// </summary>
public class ScorePanel : MonoBehaviour
{
		public Text previousScoreText;
		public Text bestScoreText;

		// Use this for initialization
		void Start ()
		{
				try {
						//Apply the player data on the previous score ,best score texts
						if (previousScoreText != null && bestScoreText != null) {
								DataManager.PlayerData playerData = DataManager.LoadPlayerData ();
								if (playerData != null) {
										previousScoreText.text = playerData.previousScore.ToString ();
										bestScoreText.text = playerData.bestScore.ToString ();
								}
						}
				} catch (Exception ex) {
						Debug.Log (ex.Message);
				}
		}
}