using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _movingPanel;
    [SerializeField] private GameObject _movingButton;
    [SerializeField] private GameObject _attackButton;
    [SerializeField] private PlayerInteractions _playerInteractions;

    public void DeactivateMovingPossibilitiy()
    {
        _movingPanel.SetActive(false);
        _movingButton.SetActive(false);
        _playerInteractions.SetMovingDirectoin(Vector2.zero);
        _playerInteractions.SetPlayerMoving(false);
    }

    public void ActivateMovingPossibility()
    {
        _movingPanel.SetActive(true);
        _movingButton.gameObject.SetActive(true);
    }

    public void SetAttackButtonActive(bool shouldBeActive) => _attackButton.SetActive(shouldBeActive);
}
