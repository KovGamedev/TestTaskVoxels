using UnityEngine;

public class Axe : MonoBehaviour, IAttacking
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationName;

    public void Attack() => _animator.Play(_animationName, 0, 0);
}
