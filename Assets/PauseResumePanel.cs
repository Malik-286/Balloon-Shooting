using hardartcore.CasualGUI;
using UnityEngine;

public class PauseResumePanel : MonoBehaviour
{
   

    public void ResumeGame()
    {
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
        
      // go to main menu 

    }






}
