using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Beholder : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationSpeedVariable;
    [Header("Eyes defence")]
    [SerializeField] private List<BeholderEye> _eyes = new();
    [SerializeField] private float _attackingTime;
    [SerializeField] private Collider _triggerCollider;
    [Header("Attack")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _shootingInterval;
    [SerializeField] private Vector3 _projectileSpawnDeltaPositoin;
    [Header("Destroying")]
    [SerializeField] private float _dethThresold;
    [SerializeField] private float _fallDuration;
    [SerializeField] private float _fallAltitudeY;
    [SerializeField] private Ease _fallEasing;
    [SerializeField] private UnityEvent _destroyingEvent = new();
    [Header("Audio")]
    [SerializeField] private AudioClip _painSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioSource _audioSource;

    public void StartAnimations()
    {
        _animator.SetFloat(_animationSpeedVariable, 1);
        StartCoroutine(Attack());
    }

    public void OnEyeDamaged()
    {
        var isAlive = _animator.enabled;
        if(isAlive)
            _audioSource.Play();

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
            StopAllCoroutines();
            transform.DOMoveY(_fallAltitudeY, _fallDuration)
                .SetEase(_fallEasing)
                .OnComplete(() => _destroyingEvent.Invoke())
                .Play();
            _audioSource.clip = _deathSound;
            _audioSource.Play();

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
            StartCoroutine(StopDefenceCoroutine());
        }
    }

    private IEnumerator StopDefenceCoroutine()
    {
        yield return new WaitForSeconds(_attackingTime);
        foreach(var eye in _eyes) {
            eye.StopAttack();
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(_shootingInterval);
        var projectile = Instantiate(_projectilePrefab);
        projectile.transform.position = transform.position + _projectileSpawnDeltaPositoin;
        StartCoroutine(Attack());
    }
}
