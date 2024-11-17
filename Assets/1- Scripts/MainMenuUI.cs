using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class MainMenuUI : MonoBehaviour
{


    [SerializeField] GameObject settingsPanel;
    [SerializeField] TextMeshProUGUI versionText;

    void Start()
    {
        settingsPanel.SetActive(false);
        UpdateGameVersionText();

        Time.timeScale = 1;
    }

    public void StartGame()
    {
        Invoke(nameof(SceneChange), 4f);    }

    public void SceneChange()
    {
        SceneManager.LoadScene(1);
    }

    public void MuteAndUnMuteAudio()
    {
        if(AudioManager.Instance.audioSource != null)
        {
            if(AudioManager.Instance.audioSource.mute)
            {
                AudioManager.Instance.audioSource.mute = false;
            }else if(!AudioManager.Instance.audioSource.mute)
            {
                AudioManager.Instance.audioSource.mute = true;

            }
        }

    }


    void UpdateGameVersionText()
    {
        versionText.text = Application.version;
    }
   
}
