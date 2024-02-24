using DG.Tweening;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _movingPanel;
    [SerializeField] private MoveButton _movingButton;
    [SerializeField] private GameObject _attackButton;
    [SerializeField] private PlayerInteractions _playerInteractions;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private float _congratulationsRevealingDuration;
    [SerializeField] private float _congratulationsDuration;

    public void DeactivateMovingPossibilitiy()
    {
        _movingPanel.SetActive(false);
        _movingButton.gameObject.SetActive(false);
        _movingButton.ResetPosition();
        _playerInteractions.SetMovingDirectoin(Vector2.zero);
        _playerInteractions.SetPlayerMoving(false);
    }

    public void ActivateMovingPossibility()
    {
        _movingPanel.SetActive(true);
        _movingButton.gameObject.SetActive(true);
    }

    public void SetAttackButtonActive(bool shouldBeActive) => _attackButton.SetActive(shouldBeActive);

    public void ShowCongratulations() {
        DOTween.Sequence()
          .Insert(0, _textMesh.DOFade(1, _congratulationsRevealingDuration))
          .Insert(_congratulationsRevealingDuration + _congratulationsDuration, _textMesh.DOFade(0, _congratulationsRevealingDuration))
          .Play();
    }
}
