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
    [SerializeField] int TotalArrows;

    [Header("Balloons")]
    public int[] BalloonsCountPerLevel;
    public int SmashedBallons;
    public TextMeshProUGUI BalloonsCounter;

    [Header("Others")]
    public TextMeshProUGUI LevelCounter;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //Gameplay Essentials
        LevelCounter.text = (PlayerPrefs.GetInt("CurrentLevel")+1).ToString();
        TotalArrows = ArrowsCountPerLevel[PlayerPrefs.GetInt("CurrentLevel")];
    }
    // Start is called before the first frame update
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
    public void UpdateArrowsCounter()
    {
            ArrowsCounter.text = RemainingArrows.ToString(); 
        if(RemainingArrows == 0)
        {
            Fail = true;
            Success = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!PanelsActivated)
        {
            if (Success)
            {
                if (GamePlayUI.Instance)
                {
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
}
