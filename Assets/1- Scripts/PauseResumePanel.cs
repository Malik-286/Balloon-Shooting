using hardartcore.CasualGUI;
using UnityEngine.SceneManagement;

using UnityEngine;

public class PauseResumePanel : MonoBehaviour
{
   

    public void ResumeGame()
    {

        if (BowController.instance)
        {
            BowController.instance.gameObject.GetComponent<BowController>().enabled = true;
        }

        Time.timeScale = 1.0f;
        gameObject.GetComponent<Dialog>().HideDialog();

    }

    public void FreeFiveArrows()
    {
        Debug.Log("Five Rewarded arrows...");
           // implement rewarded video ad here to give user 5 rewards 
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);

    }






}
