using System;
using System.Linq;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentController : MonoBehaviour
{
    public MainGameController MainGameController;

    [BoxGroup("UI")]
    public Slider ProgressBar;
    [BoxGroup("UI")]
    public Image IconLast;
    [BoxGroup("UI")]
    public Image IconNext;

    [TableList] public LevelEnvironment[] Environments;

    [Serializable]
    public struct LevelEnvironment
    {
        [PreviewField(ObjectFieldAlignment.Center), TableColumnWidth(55, false)]
        public Sprite icon;
        public GameObject environmentObject;
        [TableColumnWidth(100, false)]
        public int level;
    }

    private void Start()
    {
        MainGameController.CurrentLevel.Subscribe(UpdateEnvironmentForLevel).AddTo(this);
    }

    private void UpdateEnvironmentForLevel(int levelIndex)
    {
        int currentIndex = Environments
            .Select((environment, i) => (environment.level, index: i))
            .Last(tuple => tuple.level <= levelIndex).index;

        ApplyEnvironment(currentIndex);
        UpdateUI(levelIndex, currentIndex);
    }

    private void UpdateUI(int levelIndex, int environmentIndex)
    {
        float fillValue = 1;
        if (environmentIndex + 1 < Environments.Length)
        {
            int leftBorder = Environments[environmentIndex].level;
            int rightBorder = Environments[environmentIndex + 1].level;
            fillValue = Mathf.InverseLerp(leftBorder, rightBorder, levelIndex);
        }

        ProgressBar.value = fillValue;
        IconLast.sprite = Environments[environmentIndex].icon;
        int indexNext = Mathf.Clamp(environmentIndex + 1, 0, Environments.Length);
        IconNext.sprite = Environments[indexNext].icon;
    }

    private void ApplyEnvironment(int index)
    {
        foreach (LevelEnvironment environment in Environments)
        {
            environment.environmentObject.SetActive(false);
        }
        
        Environments[index].environmentObject.SetActive(true);
    }
}