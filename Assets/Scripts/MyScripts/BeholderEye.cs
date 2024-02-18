using UnityEngine;

public class BeholderEye : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _layerName;

    public void StopAnimation(Vector3 destructionPoint, float relativeVelocity, float destructionPercentage)
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex(_layerName), 0);
    }
}
