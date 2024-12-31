using hardartcore.CasualGUI;
using UnityEngine;
using UnityEngine.Purchasing;


public class Store : MonoBehaviour
{
    
    [SerializeField] GameObject restoreButton;
    [SerializeField] GameObject removeAdsButton;



    const string removeAds_ProductID = "com.agsventures.balloonblasterquest.removeads";


    [Header("Ads Status")]

    public string adsStatus;
    public string defaultAdsStatus = "enabled";

    void Awake()
    {

        CheckRestoreButton();

        adsStatus = PlayerPrefs.GetString("AdsStatusKey");
        if (adsStatus == string.Empty)
        {
            this.adsStatus = "enabled";
        }
        if (this.adsStatus == "disabled")
        {
            removeAdsButton.SetActive(false);
        }

    }

    void CheckRestoreButton()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            restoreButton.SetActive(false);
        }
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == removeAds_ProductID)
        {
            adsStatus = "disabled";
            PlayerPrefs.SetString("AdsStatusKey", adsStatus);
          //  purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();
        }
         

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
       // purchaseFailedPanel.GetComponent<Dialog>().ShowDialog();
    }


    

}
