using UnityEngine;
using TMPro;
using System.Collections;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loadingText;


     void Start()
    {
        StartCoroutine(StartLoading());
    }

    IEnumerator StartLoading()
    {
        yield return new WaitForSeconds(0.25f);

        loadingText.text = "Loading.";

        yield return new WaitForSeconds(0.25f);
        loadingText.text = "Loading..";


        yield return new WaitForSeconds(0.25f);
        loadingText.text = "Loading...";

        yield return new WaitForSeconds(0.25f);
        loadingText.text = "Loading.";

        yield return new WaitForSeconds(0.25f);

    }

}
