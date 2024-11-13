using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;


[DisallowMultipleComponent]
public class DataManager : MonoBehaviour
{
		/// <summary>
		/// The UI score text.
		/// </summary>
		public static Text scoreText;

		/// <summary>
		/// The UI arrows text.
		/// </summary>
		private static Text arrowsText;

		/// <summary>
		/// The current score.
		/// </summary>
		private static int currentScore;

		/// <summary>
		/// The number of arrows.
		/// </summary>
		private static int numberOfArrows;

		/// <summary>
		/// The player data reference.
		/// </summary>
		private static PlayerData playerData;

		/// <summary>
		/// Whether to initiate/load the player's on awake
		/// </summary>
		public bool initiatePlayerDataOnAwake = true;

		/// <summary>
		/// Whether to set the default data for the score and number of arrows on awake.
		/// </summary>
		public bool setDefaultDataOnAwake = true;

		void Awake ()
		{
				#if UNITY_IPHONE
					//Enable the MONO_REFLECTION_SERIALIZER(For IOS Platform Only)
					System.Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
				#endif

				if (initiatePlayerDataOnAwake) {

						//Load player data from the file
						playerData = LoadPlayerData ();
						if (playerData == null) {
								//Define new player data
								playerData = new PlayerData ();
								//Save it to the file
								SavePlayerData ();
						}
				}

				if (setDefaultDataOnAwake) {

						//Setting up the references
						if (scoreText == null) {
								//scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
						}

						if (arrowsText == null) {
								//arrowsText = GameObject.Find ("ArrowsText").GetComponent<Text> ();
						}

						//Reset the current score
						ResetCurrentScore ();
						//Reset the number of arrows
						ResetNumberOfArrows ();

						//Apply the score on the score's UI text
						ApplyCurrentScoreOnUI ();
						//Apply the number of arrows on the arrow's UI text
						ApplyNumberOfArrowsOnUI ();
				}
		}

		/// <summary>
		/// Apply the current score on score's UI text.
		/// </summary>
		private static void ApplyCurrentScoreOnUI ()
		{
				if (scoreText == null) {
						return;
				}
				scoreText.text = "Score : " + currentScore;
		}

		/// <summary>
		/// Applies the number of arrows on arrow's UI text.
		/// </summary>
		private static void ApplyNumberOfArrowsOnUI ()
		{
				if (arrowsText == null) {
						return;
				}
				arrowsText.text = "Arrows : " + numberOfArrows;
		}

	/// <summary>
	/// Reset the number of arrows.
	/// </summary>
	public static void ResetNumberOfArrows()
	{
		if (MissionManager.Instance)
		{
			numberOfArrows = MissionManager.Instance.ArrowsCountPerLevel[PlayerPrefs.GetInt("CurrentLevel")];
		}
	}

		/// <summary>
		/// Reset the current score.
		/// </summary>
		public static void ResetCurrentScore ()
		{
				currentScore = 0;
		}

		///PlayerData class
		[System.Serializable]
		public class PlayerData
		{
				public int bestScore;
				public int previousScore;
		}

		/// <summary>
		/// Save the player data to the file.
		/// </summary>
		public static void SavePlayerData ()
		{
				Debug.Log ("Saving Player Data...");

				if (playerData == null) {
						Debug.Log ("Null Data");
						return;
				}

				PlayerPrefs.SetInt ("bestScore",playerData.bestScore);
				PlayerPrefs.SetInt ("previousScore",playerData.previousScore);
		}

		/// <summary>
		/// Load the player data from the file.
		/// </summary>
		/// <returns>The player data.</returns>
		public static PlayerData LoadPlayerData ()
		{
				Debug.Log ("Loading Player Data...");
		
				PlayerData playerData = new PlayerData();
				playerData.bestScore = PlayerPrefs.GetInt ("bestScore");
				playerData.previousScore = PlayerPrefs.GetInt ("previousScore");
				return playerData;
		}

		public static void SaveNewScore ()
		{
				playerData.previousScore = currentScore;
				if (currentScore > playerData.bestScore) {
						playerData.bestScore = currentScore;
				}
				SavePlayerData ();
		}

		/// <summary>
		/// Get or set the current score.
		/// </summary>
		/// <value>The current score.</value>
		public static int CurrentScore {
				get { return currentScore;}
				set {
						currentScore = value;
						ApplyCurrentScoreOnUI ();
				} 
		}

		/// <summary>
		/// Get or set the number of arrows.
		/// </summary>
		/// <value>The number of arrows.</value>
		public static int NumberOfArrows {
				get { return numberOfArrows;}
				set {
						numberOfArrows = value;
            //Apply number of arrows on arrow's UI text
            ApplyNumberOfArrowsOnUI	 ();
				}
		}
}