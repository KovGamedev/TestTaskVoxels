using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnlockedGirlsList : MonoBehaviour
    {
        [SerializeField] NewGirlsLevelUpController girlsLevelUpController;
        [SerializeField] private Image[] girlImages;
        [SerializeField] private List<Button> progressDots;
        [SerializeField] private Button buttonBack;
        [SerializeField] private Button buttonNext;

        private const int PAGE_SIZE = 9;

        private Sprite[] _unlockedGirls;
        private int _pagesCount = 1;
        private int _activePage = 0;

        private void Start()
        {
            buttonBack.onClick.AddListener(BackAction);
            buttonNext.onClick.AddListener(NextAction);
        }

        public void Open()
        {
            _unlockedGirls = girlsLevelUpController.GetUnlockedGirls().Select(girl => girl.Icon).ToArray();
            _pagesCount = Mathf.CeilToInt(1.0f * _unlockedGirls.Length / PAGE_SIZE);
            _activePage = 0;
            UpdateProgressDots();
        }

        private void UpdateProgressDots()
        {
            progressDots.Skip(1).ForEach(button => Destroy(button.gameObject));
            progressDots.RemoveRange(1, progressDots.Count - 1);
            Button prefab = progressDots[0];
            prefab.interactable = false;
            for (int i = 1; i < _pagesCount; i++)
            {
                progressDots.Add(Instantiate(prefab, prefab.transform.parent));
            }

            UpdateActivePage();
        }

        private void UpdateActivePage()
        {
            progressDots.ForEach(dot => dot.interactable = false);
            progressDots[_activePage].interactable = true;

            int startIndex = _activePage * PAGE_SIZE;
            int remainingGirlsCount = _unlockedGirls.Length - startIndex;
            int girlsCount = Mathf.Min(remainingGirlsCount, PAGE_SIZE);
            var girlsToDraw = _unlockedGirls.AsSpan(startIndex, girlsCount);
            for (int i = 0; i < girlImages.Length; i++)
            {
                girlImages[i].color = i < girlsCount ? Color.white : Color.clear;
                if (i < girlsCount)
                    girlImages[i].sprite = girlsToDraw[i];
            }
        }

        private void NextAction()
        {
            buttonNext.transform.DOKill(true);
            Vector3 targetScale = buttonNext.transform.localScale * 1.1f;
            buttonNext.transform.DOScale(targetScale, 0.1f).SetLoops(2, LoopType.Yoyo);
            _activePage = (_activePage + 1) % _pagesCount;
            UpdateActivePage();
        }

        private void BackAction()
        {
            buttonBack.transform.DOKill(true);
            Vector3 targetScale = buttonBack.transform.localScale * 1.1f;
            buttonBack.transform.DOScale(targetScale, 0.1f).SetLoops(2, LoopType.Yoyo);
            _activePage = (_pagesCount + _activePage - 1) % _pagesCount;
            UpdateActivePage();
        }
    }
}