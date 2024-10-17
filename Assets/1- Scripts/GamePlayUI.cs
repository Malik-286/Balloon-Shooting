
using hardartcore.CasualGUI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{

    public static GamePlayUI Instance;

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    [SerializeField] GameObject pauseGamePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1.0f;

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        pauseGamePanel.SetActive(false);
    }
    void Update()
    {

    }

    
    public void ActivateWinPanel()
    {
        winPanel.GetComponent<Dialog>().ShowDialog();
    }

    public void ActivateLosePanel()
    {
        losePanel.GetComponent<Dialog>().ShowDialog();
    }


    public void NextButton()
    {
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        pauseGamePanel.gameObject.GetComponent<Dialog>().ShowDialog();
        StartCoroutine(PauseTheGame());
    }


    IEnumerator PauseTheGame()
    {
        yield return new WaitForSeconds(0.4f);
        Time.timeScale = 0.0f;
    }

  
}
