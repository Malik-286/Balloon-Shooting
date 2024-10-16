using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MissionManager : MonoBehaviour
{

    public static MissionManager Instance;

    public int[] ArrowsCountPerLevel;
    public int RemainingArrows;
    public TextMeshProUGUI ArrowsCounter;
    [SerializeField] int TotalArrows;

    public int SmashedBallons;
    public TextMeshProUGUI BalloonsCounter;
    [SerializeField] bool Success,Fail;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateBalloonsCounter();
        UpdateArrowsCounter();

    }

    public void UpdateBalloonsCounter()
    {
        if(BalloonsCounter != null)
        {
            BalloonsCounter.text = SmashedBallons.ToString();
        }
    }
    public void UpdateArrowsCounter()
    {
        if (BalloonsCounter != null)
        {
            ArrowsCounter.text = RemainingArrows.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
