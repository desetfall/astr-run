using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private const string ALREADY_PUR = "Select";
    private const string ALREADY_SELECTED = "SELECTED";
    private int money, currentProductId, currentSuite;
    private string[] allSuites;
    private Animator shopPanelAnimator;
    
    [SerializeField] private Text shopMoney, mainMenuMoney;
    [SerializeField] private ProductSO[] products;
    [SerializeField] private Image productImage;
    [SerializeField] private Text productPrice;
    [SerializeField] private Button buyBtn;

    [SerializeField] private Player PlayerBehaviour;

    void Start()
    {
        shopPanelAnimator = gameObject.GetComponent<Animator>();
    }

    public void ShopInit()
    {
        money = PlayerPrefs.GetInt("money");
        currentSuite = PlayerPrefs.GetInt("currentSuite");
        allSuites = PlayerPrefs.GetString("allPlayerSuites").Split(',');

        shopMoney.text = money.ToString();
        if (currentSuite != 9999)
        {
            FillProductCard(products[currentSuite]);
        }
        else
        {
            currentProductId = 0;
            FillProductCard(products[currentProductId]);
        }
        shopPanelAnimator.SetTrigger("ShopInst");
    }

    public void ShopClose()
    {
        mainMenuMoney.text = money.ToString();
        PlayerBehaviour.Invoke("SetSkin", 0);
        shopPanelAnimator.SetTrigger("ShopStop");        
    }

    public void NextProduct()
    {
        if (currentProductId < products.Length - 1)
        {
            FillProductCard(products[currentProductId + 1]);
        }
    }

    public void PreviousProduct()
    {
        if (currentProductId > 0)
        {
            FillProductCard(products[currentProductId - 1]);
        }
    }

    public void BuyProduct()
    {
        if (CheckSuitePurchased(currentProductId))
        {
            currentSuite = currentProductId;
            PlayerPrefs.SetInt("currentSuite", currentSuite);
            FillProductCard(products[currentSuite]);
        }
        else
        {
            int price = int.Parse(products[currentProductId].Price);
            if (money >= price)
            {
                money -= price;
                PlayerPrefs.SetInt("money", money);
                shopMoney.text = money.ToString();

                string newSuites = PlayerPrefs.GetString("allPlayerSuites");
                newSuites += currentProductId.ToString() + ",";
                PlayerPrefs.SetString("allPlayerSuites", newSuites);
                allSuites = PlayerPrefs.GetString("allPlayerSuites").Split(',');

                currentSuite = currentProductId;
                PlayerPrefs.SetInt("currentSuite", currentSuite);
                
                FillProductCard(products[currentProductId]);
            }
        }
    }

    void FillProductCard(ProductSO card)
    {
        currentProductId = card.ProductID;
        if (currentProductId == currentSuite)
        {
            productPrice.text = ALREADY_SELECTED;
            buyBtn.enabled = false;
        }
        else
        {
            productPrice.text = CheckSuitePurchased(currentProductId) ? ALREADY_PUR : card.Price;
            buyBtn.enabled = true;
        }
        productImage.sprite = card.ProductIcon;
    }

    bool CheckSuitePurchased(int suiteID)
    {
        foreach (string str in allSuites)
        {
            if (str == suiteID.ToString())
            {
                return true;
            }
        }
        return false;
    }
}
