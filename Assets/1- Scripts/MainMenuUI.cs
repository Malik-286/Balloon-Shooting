using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
     void Start()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

   
}
