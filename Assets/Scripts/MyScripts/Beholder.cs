using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationSpeedVariable;
    [Header("Eyes defence")]
    [SerializeField] private List<BeholderEye> _eyes = new();
    [SerializeField] private float _attackingTime;
    [SerializeField] private Collider _triggerCollider;
    [Header("Destroying")]
    [SerializeField] private float _dethThresold;
    [SerializeField] private float _fallDuration;
    [SerializeField] private float _fallAltitudeY;
    [SerializeField] private Ease _fallEasing;

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

    public void OnDeath(Vector3 destructionPoint, float relativeVelocity, float destructionPercentage)
    {
        var isAlive = _animator.enabled;
        if(isAlive && _dethThresold <= destructionPercentage) {
            _animator.enabled = false;
            transform.DOMoveY(_fallAltitudeY, _fallDuration)
                .SetEase(_fallEasing)
                .Play();
        }
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
