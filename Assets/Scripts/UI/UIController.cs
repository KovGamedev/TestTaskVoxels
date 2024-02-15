using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject WinUI;
    public GameObject LoseUI;
    public GameObject InGameUI;

    [FoldoutGroup("StartUI")] public TMP_Text MoneyAmountStart;

    [FoldoutGroup("InGameUI")] public Slider ProgressSlider;
    [FoldoutGroup("InGameUI")] public Slider AllyHealthSlider;
    [FoldoutGroup("InGameUI")] public Slider EnemyHealthSlider;
    [FoldoutGroup("InGameUI")] public TMP_Text LevelNumber;
    [FoldoutGroup("InGameUI")] public TMP_Text MoneyAmountInGame;

    [FoldoutGroup("WinUI")] public TMP_Text WinLevelNumber;

    private Animator _startUIAnimator;
    private Animator _winUIAnimator;
    private Animator _loseUIAnimator;
    private Animator _inGameUIAnimator;

    private string[] _triggers;

    private void Awake()
    {
        _triggers = new string[4];
        _startUIAnimator = StartUI.GetComponent<Animator>();
        _winUIAnimator = WinUI.GetComponent<Animator>();
        _loseUIAnimator = LoseUI.GetComponent<Animator>();
        _inGameUIAnimator = InGameUI.GetComponent<Animator>();
        StartUI.SetActive(true);
        WinUI.SetActive(true);
        LoseUI.SetActive(true);
        InGameUI.SetActive(true);
    }

    public void SetUI(UIType type)
    {
        for (int i = 0; i < _triggers.Length; i++)
        {
            _triggers[i] = "Hide";
        }

        switch (type)
        {
            case UIType.Start:
                _triggers[0] = "Show";
                break;
            case UIType.InGame:
                _triggers[1] = "Show";
                break;
            case UIType.Win:
                _triggers[2] = "Show";
                break;
            case UIType.Lose:
                _triggers[3] = "Show";
                break;
        }

        _startUIAnimator.SetTrigger(_triggers[0]);
        _inGameUIAnimator.SetTrigger(_triggers[1]);
        _winUIAnimator.SetTrigger(_triggers[2]);
        _loseUIAnimator.SetTrigger(_triggers[3]);
    }

    public void SetMoneyAmount(float amount)
    {
        MoneyAmountInGame.text = amount.ToString();
        MoneyAmountStart.text = amount.ToString();
    }

    public void SetSliderAmount(float amount)
    {
        ProgressSlider.value = amount;
    }

    public void SetLevelNumber(int number)
    {
        LevelNumber.text = $"Level {number + 1}";
    }

    public void SetWinLevelNumber(int number)
    {
        WinLevelNumber.text = $"Level {number + 1}";
    }

    public void SubscribeFight(IFightController fightController)
    {
        fightController.OnAllyHealthInit += value => AllyHealthSlider.value = AllyHealthSlider.maxValue = value;
        fightController.OnAllyHealthUpdate += value => AllyHealthSlider.value = value;
        fightController.OnEnemyHealthInit += value => EnemyHealthSlider.value = EnemyHealthSlider.maxValue = value;
        fightController.OnEnemyHealthUpdate += value => EnemyHealthSlider.value = value;
    }
}

public enum UIType
{
    Start = 1,
    InGame = 2,
    Win = 3,
    Lose = 4
}