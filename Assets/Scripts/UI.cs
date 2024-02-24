using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _movingPanel;
    [SerializeField] private GameObject _movingButton;

    public void DeactivateMovingPossibility()
    {
        _movingPanel.SetActive(false);
        _movingButton.SetActive(false);
    }

    public void ActivateMovingPossibility()
    {
        _movingPanel.SetActive(true);
        _movingButton.SetActive(true);
    }
}
