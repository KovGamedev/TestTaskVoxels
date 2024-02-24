using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BeholderEye : MonoBehaviour
{
    [SerializeField] private UnityEvent _damageEvent = new();
    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _layerName;
    [Header("Ray")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _lineSourcePoint;

    private Vector3 _targetGlobalPosition;

    public void StopAnimation(Vector3 destructionPoint, float relativeVelocity, float destructionPercentage)
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex(_layerName), 0);
        _damageEvent.Invoke();
    }

    public void StartAttack(Vector3 targetGlobalPosition)
    {
        _targetGlobalPosition = targetGlobalPosition;
        if(IsHealthy())
            _lineRenderer.enabled = true;
    }

    public bool IsHealthy() => 0 < _animator.GetLayerWeight(_animator.GetLayerIndex(_layerName));

    public void StopAttack() => _lineRenderer.enabled = false;

    private void Update()
    {
        if(_lineRenderer.enabled)
            _lineRenderer.SetPosition(1, transform.InverseTransformPoint(_targetGlobalPosition));
    }
}
