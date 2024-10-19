using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{



		public void CheckArrowsNumber ()
		{
				if (DataManager.NumberOfArrows == 0) {

						//Save the new score
						try {
								DataManager.SaveNewScore ();
						} catch (Exception ex) {
								Debug.Log (ex.Message);
						}

						//StartCoroutine ("LoadMainMenuScene");
				}
		}

	public IEnumerator LoadMainMenuScene()
	{
		yield return new WaitForSeconds(2.5f);
		SceneManager.LoadScene(0);
	}

	 

}