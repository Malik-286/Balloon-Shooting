
using hardartcore.CasualGUI;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;



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
}
