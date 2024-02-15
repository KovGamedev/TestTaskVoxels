using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour
{
    public GameObject[] Levels;

    [NonSerialized] public readonly IntReactiveProperty CurrentLevel = new IntReactiveProperty();

    private UIController _uiController;
    private CameraController _cameraController;
    private NewFightController _fightController;
    private TutorialController _tutorialController;
    private NewCameraRaycastController _cameraRaycastController;

    private string _currentLevel = "Currentlevefeedwqq";
    private string _absoluteLevel = "LevelsfefweCqq";

    private int _currentEnemyAmount;
    private int _enemiesInLift;

    private bool _gameStarted;

    [Button]
    private void CleanSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Awake()
    {
        BuildLevel();
        _fightController = GetComponent<NewFightController>();
        _cameraController = GetComponent<CameraController>();
        _tutorialController = GetComponent<TutorialController>();
        _cameraRaycastController = GetComponent<NewCameraRaycastController>();
        _uiController = FindObjectOfType<UIController>();
    }

    private void Start()
    {
        _cameraRaycastController.ActivateController(true);
        _uiController.SetLevelNumber(PlayerPrefs.GetInt(_absoluteLevel));
        _uiController.SetUI(UIType.Start);
        _uiController.SubscribeFight(_fightController);
        _tutorialController.StartTutorial();
    }

    public void StartGame()
    {
        _fightController.StartFighting();
        _cameraController.StartCameraMovement();
        _gameStarted = true;
        _uiController.SetUI(UIType.InGame);
    }

    private void BuildLevel()
    {
        foreach (GameObject level in Levels)
        {
            level.SetActive(false);
        }

        int currentLevel = PlayerPrefs.GetInt(_currentLevel);
        if (currentLevel >= Levels.Length)
        {
            currentLevel = 10;
            PlayerPrefs.SetInt(_currentLevel, 10);
            PlayerPrefs.Save();
        }

        Levels[currentLevel].SetActive(true);
        CurrentLevel.Value = currentLevel;
    }

    public void LevelLose()
    {
        Invoke(nameof(TurnOnLoseUI), 2f);
    }

    public void LevelWin()
    {
        _uiController.SetWinLevelNumber(PlayerPrefs.GetInt(_absoluteLevel));
        Savelevel();
        Invoke(nameof(MakeParticles), 1f);
        Invoke(nameof(TurnOnWinUI), 2f);
    }

    private void MakeParticles()
    {
        ParticlesManager.Instance.MakeConfettiParticles();
    }

    private void TurnOnWinUI()
    {
        _uiController.SetUI(UIType.Win);
    }

    private void TurnOnLoseUI()
    {
        _uiController.SetUI(UIType.Lose);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Savelevel()
    {
        if (PlayerPrefs.GetInt(_currentLevel) + 1 == Levels.Length)
        {
            PlayerPrefs.SetInt(_currentLevel, 0);
            PlayerPrefs.SetInt(_absoluteLevel, PlayerPrefs.GetInt(_absoluteLevel) + 1);
        }
        else
        {
            PlayerPrefs.SetInt(_currentLevel, PlayerPrefs.GetInt(_currentLevel) + 1);
            PlayerPrefs.SetInt(_absoluteLevel, PlayerPrefs.GetInt(_absoluteLevel) + 1);
        }
    }
}