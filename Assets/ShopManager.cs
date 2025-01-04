using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public GameObject[] ShopItemButtons;//Start from 1st bow as zero
    public int[] Prices;
    [SerializeField] int TotalShopItems = 10;

    private void Awake()
    {

        for (int i = 0; i < TotalShopItems; i++)
        {
            if(PlayerPrefs.GetInt("ItemUnlocked" + i) == 1)
            {
                ShopItemButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                ShopItemButtons[i].transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void BuyItem(int ShopItemNumber)//Start from 1st bow as zero
    {
        if (Prices[ShopItemNumber] >= PlayerPrefs.GetInt("CurrentGold"))
        {
            PlayerPrefs.SetInt("CurrentGold", PlayerPrefs.GetInt("CurrentGold") - Prices[ShopItemNumber]);
            ShopItemButtons[ShopItemNumber].transform.GetChild(1).gameObject.SetActive(false);
            ShopItemButtons[ShopItemNumber].transform.GetChild(2).gameObject.SetActive(true);
            ShopItemButtons[ShopItemNumber].transform.GetChild(3).gameObject.SetActive(false);
            PlayerPrefs.SetInt("ItemUnlocked" + ShopItemNumber, 1);
        }
        else
        {

        }
    }

    public void SelectItem(int ShopItemNumber)
    {
        for (int i = 0; i < TotalShopItems; i++)
        {
            if (PlayerPrefs.GetInt("ItemUnlocked" + i) == 1)
            {
                for (int s = 0; s < ShopItemButtons.Length; s++)
                {
                    ShopItemButtons[s].transform.GetChild(2).gameObject.SetActive(false);
                }
                ShopItemButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                ShopItemButtons[i].transform.GetChild(2).gameObject.SetActive(true);
                ShopItemButtons[i].transform.GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("SelectedItem", ShopItemNumber);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
