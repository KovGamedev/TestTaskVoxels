using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _rectTransfrom;
    [SerializeField] private float _maxDeltaPosition;
    [SerializeField] private PlayerInteractions _playerInterations;

    private Vector2 _startAnchoredPosition;

    public void OnBeginDrag(PointerEventData eventData) {
        _playerInterations.SetPlayerMoving(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var scaledDelta = eventData.delta / _canvas.scaleFactor;
        var futurePosition = _rectTransfrom.anchoredPosition + scaledDelta;
        if(Vector2.Distance(_startAnchoredPosition, futurePosition) < _maxDeltaPosition) {
            _rectTransfrom.anchoredPosition += scaledDelta;
            _playerInterations.SetMovingDirectoin((_rectTransfrom.anchoredPosition - _startAnchoredPosition) / _maxDeltaPosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rectTransfrom.anchoredPosition = _startAnchoredPosition;
        _playerInterations.SetPlayerMoving(false);
        _playerInterations.SetMovingDirectoin(Vector2.zero);
    }

    public void ResetPosition() => _rectTransfrom.anchoredPosition = _startAnchoredPosition;

    private void Awake()
    {
        _startAnchoredPosition = _rectTransfrom.anchoredPosition;
    }
}
