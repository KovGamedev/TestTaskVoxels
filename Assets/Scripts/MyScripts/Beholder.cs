using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationSpeedVariable;
    [SerializeField] private List<BeholderEye> _eyes = new();
    [SerializeField] private float _attackingTime;
    [SerializeField] private Collider _triggerCollider;

    public void StartAnimations() => _animator.SetFloat(_animationSpeedVariable, 1);

    public void OnEyeDamaged()
    {
        var areAllEyesDamaged = true;
        foreach(var eye in _eyes) {
            if(eye.IsHealthy()) {
                areAllEyesDamaged = false;
                return;
            }
        }

        if(areAllEyesDamaged)
            _triggerCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        var projectile = collider.GetComponentInParent<MagicProjectile>();
        if(projectile != null) {
            foreach(var eye in _eyes) {
                eye.StartAttack(projectile.transform.position);
            }
            Destroy(projectile.gameObject);
            StartCoroutine(StopAttackCoroutine());
        }
    }

    private IEnumerator StopAttackCoroutine()
    {
        yield return new WaitForSeconds(_attackingTime);
        foreach(var eye in _eyes) {
            eye.StopAttack();
        }
    }
}
