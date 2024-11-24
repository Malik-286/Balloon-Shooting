using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using NiobiumStudios;



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
    void OnEnable()
    {
        DailyRewards.instance.onClaimPrize += OnClaimPrizeDailyRewards;
    }

    void OnDisable()
    {
        DailyRewards.instance.onClaimPrize -= OnClaimPrizeDailyRewards;
    }

    // this is your integration function. Can be on Start or simply a function to be called
    public void OnClaimPrizeDailyRewards(int day)
    {
        //This returns a Reward object
        Reward myReward = DailyRewards.instance.GetReward(day);

        // And you can access any property
        print(myReward.unit);   // This is your reward Unit name
        print(myReward.reward); // This is your reward count

        var rewardsCount = PlayerPrefs.GetInt("MY_REWARD_KEY", 0);
        rewardsCount += myReward.reward;

        PlayerPrefs.SetInt("MY_REWARD_KEY", rewardsCount);
        PlayerPrefs.Save();
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
