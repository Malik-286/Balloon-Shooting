
using hardartcore.CasualGUI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{

    public static GamePlayUI Instance;

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
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
}
