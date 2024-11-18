using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 

public class MissionManager : MonoBehaviour
{

    public static MissionManager Instance;

    public bool PanelsActivated = false;
    [SerializeField] bool Success,Fail = false;

    [Header("Arrows")]
    public int[] ArrowsCountPerLevel;
    public int RemainingArrows;
    public TextMeshProUGUI ArrowsCounter;
    public TextMeshProUGUI TotalArrowsText;
    public int TotalArrows;


    [Header("Balloons")]
    public int[] BalloonsCountPerLevel;
    public int SmashedBallons;
    public TextMeshProUGUI BalloonsCounter;
    public TextMeshProUGUI TotalBalloonsText;

    [Header("Others")]
    public TextMeshProUGUI LevelCounter;
    public TextMeshProUGUI FailReason;
    public GameObject RewardPanel;
    public TextMeshProUGUI RewardAdditionText;

    [Header("Particles and Positions")]

    [SerializeField] GameObject winParticles;
    [SerializeField] Transform winParticlesPosition;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //Gameplay Essentials
        TotalArrowsText.text = ArrowsCountPerLevel[PlayerPrefs.GetInt("CurrentLevel")].ToString();
        TotalBalloonsText.text = BalloonsCountPerLevel[PlayerPrefs.GetInt("CurrentLevel")].ToString();
        LevelCounter.text = (PlayerPrefs.GetInt("CurrentLevel")+1).ToString();
        TotalArrows = ArrowsCountPerLevel[PlayerPrefs.GetInt("CurrentLevel")];
    }

    void Start()
    {
        RemainingArrows = TotalArrows;
        UpdateArrowsCounter();
    }

    public void UpdateBalloonsCounter()
    {
        BalloonsCounter.text = SmashedBallons.ToString();
        if(SmashedBallons == BalloonsCountPerLevel[PlayerPrefs.GetInt("CurrentLevel")])
        {
            Success = true;
            Fail = false;

            //Increase Level Number
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        }
    }

    public void UpdateTotalArrows()
    {
        TotalArrowsText.text = TotalArrows.ToString();
        Invoke(nameof(RemoveAnimation), 1f);
    }

    public void RemoveAnimation()
    {
        RewardAdditionText.gameObject.SetActive(false);
    }

    public void UpdateArrowsCounter()
    {
            ArrowsCounter.text = RemainingArrows.ToString(); 
        if(RemainingArrows == 0)
        {
            Invoke(nameof(CheckFails), 3f);
        }
    }

    public void CheckFails()
    {
        if (RemainingArrows == 0)
        {
            Fail = true;
            Success = false;

            FailReason.text = (BalloonsCountPerLevel[PlayerPrefs.GetInt("CurrentLevel")] - SmashedBallons + " BALLOONS LEFT.").ToString();
        }
    }
    void Update()
    {
        if (!PanelsActivated)
        {
            if (Success)
            {
                if (GamePlayUI.Instance)
                {
                    CreateWinParticles();
                   
                    GamePlayUI.Instance.ActivateWinPanel();
                }
                PanelsActivated = true;
            }
            else if (Fail)
            {
                if (SmashedBallons != BalloonsCountPerLevel[PlayerPrefs.GetInt("CurrentLevel")])
                    {
                    if (GamePlayUI.Instance)
                    {                   
                        GamePlayUI.Instance.ActivateLosePanel();
                    }
                    else
                    {
                        if (GamePlayUI.Instance)
                        {
                             GamePlayUI.Instance.ActivateWinPanel();
                        }
                    }
                    PanelsActivated = true;
                }
            }
        }
    }


    public void CreateWinParticles()
    {
        GameObject winParticlesClone = Instantiate(winParticles, winParticlesPosition.position, Quaternion.identity);
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayWinSoundEffect();
        }
    }

 

}
