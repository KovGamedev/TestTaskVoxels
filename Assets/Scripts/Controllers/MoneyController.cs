using Sirenix.OdinInspector;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public bool HasStartMoney;
    public int StartMoney;

    public int ExtraMoney;
    private int _moneyAmount;

    private string _moneybank = "cascqq";
    private string _isFirst = "isFicceqq";
    private UIController _uiController;

    private void Start()
    {
        _uiController = FindObjectOfType<UIController>();
        if (HasStartMoney)
        {
            _moneyAmount = StartMoney;
        }
        else
        {
            _moneyAmount = PlayerPrefs.GetInt(_moneybank);
        }

        _uiController.SetMoneyAmount(_moneyAmount);
        if (PlayerPrefs.GetInt(_isFirst) == 0)
        {
            PlayerPrefs.SetInt(_isFirst, 1);
            AddMoney(StartMoney);
        }
    }

    [Button]
    public void AddMoneyExtra()
    {
        AddMoney(ExtraMoney);
    }

    public void AddMoney(int amount)
    {
        _moneyAmount += amount;
        _uiController.SetMoneyAmount(_moneyAmount);
        SaveMoney();
    }

    public void SpendMoney(int amount)
    {
        _moneyAmount -= amount;
        _uiController.SetMoneyAmount(_moneyAmount);
        SaveMoney();
    }

    public bool CanPurchase(int cost)
    {
        if (cost <= _moneyAmount)
        {
            return true;
        }

        return false;
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt(_moneybank, _moneyAmount);
    }
}