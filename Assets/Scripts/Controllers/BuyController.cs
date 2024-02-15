using System.ComponentModel;
using UnityEngine;
using TMPro;

public class BuyController : MonoBehaviour
{
    public TMP_Text PriceText;
    public int[] Prices;
    public TMP_Text RangedPriceText;
    public int[] RangedPrices;

    private int _currentPrice;
    private int _currentRangedPrice;
    private NewGirlsLevelUpController _girlsController;
    private MoneyController _moneyController;

    private string _girlsBought = "Giww";
    private string _girlsRangedBought = "Giwa";

    private void Awake()
    {
        _girlsController = GetComponent<NewGirlsLevelUpController>();
        _moneyController = GetComponent<MoneyController>();
    }

    private void Start()
    {
        UpdatePrice();
    }

    public void BuyGirl()
    {
        BuyGirl(GirlType.Melee);
    }

    public void BuyRangedGirl()
    {
        BuyGirl(GirlType.Ranged);
    }

    public void BuyGirl(GirlType girlType)
    {
        int price = girlType switch
        {
            GirlType.Melee => _currentPrice,
            GirlType.Ranged => _currentRangedPrice,
            _ => throw new InvalidEnumArgumentException()
        };

        if (_moneyController.CanPurchase(price) && _girlsController.CanAddGirl())
        {
            _girlsController.AddGirl(girlType);
            _moneyController.SpendMoney(price);
            switch (girlType)
            {
                case GirlType.Melee:
                    PlayerPrefs.SetInt(_girlsBought, PlayerPrefs.GetInt(_girlsBought) + 1);
                    break;
                case GirlType.Ranged:
                    PlayerPrefs.SetInt(_girlsRangedBought, PlayerPrefs.GetInt(_girlsRangedBought) + 1);
                    break;
            }

            UpdatePrice();
        }
    }

    private void UpdatePrice()
    {
        int priceIndex = Mathf.Clamp(PlayerPrefs.GetInt(_girlsBought), 0, Prices.Length - 1);
        _currentPrice = Prices[priceIndex];
        int rangedPriceIndex = Mathf.Clamp(PlayerPrefs.GetInt(_girlsRangedBought), 0, RangedPrices.Length - 1);
        _currentRangedPrice = RangedPrices[rangedPriceIndex];

        PriceText.text = _currentPrice.ToString();
        RangedPriceText.text = _currentRangedPrice.ToString();
    }
}