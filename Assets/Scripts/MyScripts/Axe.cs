using UnityEngine;

public class Axe : MonoBehaviour, IAttacking
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationName;
    [Header("Attacking")]
    [SerializeField] private Collider _attackingCollider;

    public void Attack() => _animator.Play(_animationName, 0, 0);

    public void EnableAttackingCollider() => _attackingCollider.enabled = true;

    public void DisableAttackingCollider() => _attackingCollider.enabled = false;
}
