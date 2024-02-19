using UnityEngine;

public class Beholder : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationSpeedVariable;

    public void StartAnimations() => _animator.SetFloat(_animationSpeedVariable, 1);
}
