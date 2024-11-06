
using hardartcore.CasualGUI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GamePlayUI : MonoBehaviour
{

    public static GamePlayUI Instance;

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    [SerializeField] GameObject pauseGamePanel;
    [SerializeField] SpriteRenderer backGroundImage;

    [SerializeField] Sprite[] backGroundSprites;

    [Header("Scene Startup Image")]
    [SerializeField] Image fadeImage;

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
        SelectRandomBackGroundSprite();

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        pauseGamePanel.SetActive(false);


        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeImageToTransparentWhite(1.5f));
    }
    void Update()
    {

    }


    public void ActivateWinPanel()
    {
        if (BowController.instance)
        {
            BowController.instance.gameObject.GetComponent<BowController>().enabled = false;
        }

        winPanel.GetComponent<Dialog>().ShowDialog();
    }

    public void ActivateLosePanel()
    {
        if (BowController.instance)
        {
            BowController.instance.gameObject.GetComponent<BowController>().enabled = false;
        }

        losePanel.GetComponent<Dialog>().ShowDialog();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayLoseSoundEffect();
        }
    }

    public void HomebuttonButton()
    {
        if (Adsmanager.Instance)
        {
            Adsmanager.Instance.ShowIntersitial();
        }
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartButton()
    {
        if (Adsmanager.Instance)
        {
            Adsmanager.Instance.ShowIntersitial();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextButton()
    {
        if (Adsmanager.Instance)
        {
            Adsmanager.Instance.ShowIntersitial();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        pauseGamePanel.gameObject.GetComponent<Dialog>().ShowDialog();
        StartCoroutine(PauseTheGame());
        if (BowController.instance)
        {
            BowController.instance.gameObject.GetComponent<BowController>().enabled = false;
        }
    }


    IEnumerator PauseTheGame()
    {
        yield return new WaitForSeconds(0.4f);
        Time.timeScale = 0.0f;
    }

    public void SelectRandomBackGroundSprite()
    {
        // Check if there are any sprites
        if (backGroundSprites.Length == 0) return;

        // Pick a random sprite
        int randomIndex = Random.Range(0, backGroundSprites.Length);
        backGroundImage.sprite = backGroundSprites[randomIndex]; // Set the sprite
    }

    IEnumerator FadeImageToTransparentWhite(float fadeDuration)
    {
        Color startColor = Color.black;
        Color targetColor = new Color(0f, 0f, 0f, 0f); // Transparent white

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = targetColor;

        Destroy(fadeImage.gameObject, 1.5f);
    }
}
