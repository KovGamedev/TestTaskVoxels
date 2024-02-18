using UnityEngine;

public class BeholderEye : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _destructionThreshold;

    public void StopAnimation(Vector3 destructionPoint, float relativeVelocity, float destructionPercentage)
    {
        Debug.Log(destructionPoint + " " + relativeVelocity + " " + destructionPercentage);
        if(destructionPercentage < _destructionThreshold)
            _animator.enabled = false;
    }
}
