using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EPOOutline;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NewSingleCharacter : MonoBehaviour
{
    public Sprite Icon;
    public Color SpawnColor;
    public Transform ParticleTransform;
    public Rigidbody Hips;
    public float Speed;
    public float Health;
    public float DamageAmount;
    public int GirlLevel;
    public bool IsEnemy;
    public bool IsRanged;

    [ShowIf("$IsRanged"), ValidateInput("@$value != null")]
    [SerializeField] private Projectile projectile;
    [ShowIf("$IsRanged"), ValidateInput("@$value > 0")]
    [SerializeField] private float projectileSpeed;
    [ShowIf("$IsRanged"), ValidateInput("@$value != null")]
    [SerializeField] private Transform projectileStartPos;

    [SerializeField] private float _approachDistance = 0.2f;
    [SerializeField] private float _attackCooldown = 1f;

    public bool IsDead { get; private set; }

    public event Action OnDeath;

    private List<Rigidbody> _ragdollRigidbodies;
    private List<Collider> _ragdollColliders;

    private Animator _animator;
    private NewFightController _fightController;
    private MoneyController _moneyController;
    private Collider _collider;
    private CharacterController _characterController;
    private Slider _healthBar;
    private float _ragdollThrowPower = 3000f;

    private NewSingleCharacter _enemy;
    private Transform _approachTarget;

    private bool _isWalking;
    private bool _isPreparing;
    private bool _isAttackOnCooldown;
    private bool _isDamageFlashing;

    private bool _hasReachedTarget;
    private NewSingleCharacter _nextInQueue;
    private event Action onContinueQueue;
    private IObservable<Unit> onContinueQueueObservable;

    private void Awake()
    {
        onContinueQueueObservable =
            Observable.FromEvent(sub => onContinueQueue += sub, sub => onContinueQueue -= sub);
        _animator = GetComponentInChildren<Animator>();
        _fightController = FindObjectOfType<NewFightController>();
        _moneyController = FindObjectOfType<MoneyController>();
        if (IsEnemy)
        {
            gameObject.tag = TagManager.GetTag(TagType.EnemyCharacter);
            Destroy(GetComponent<SingleDragableCharacter>());
            Destroy(GetComponent<Outlinable>());
        }
        else
            gameObject.tag = TagManager.GetTag(TagType.Character);

        _collider = GetComponent<Collider>();
        _characterController = GetComponent<CharacterController>();
        _healthBar = GetComponentInChildren<Slider>();
        _healthBar.maxValue = Health;
        _healthBar.value = Health;
        _healthBar.gameObject.SetActive(false);

        //ragdoll
        _ragdollRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        _ragdollColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        _ragdollColliders.Remove(_collider);

        ToggleRagdoll(false);

        GetComponentInChildren<AttackAnimationEvent>().OnAttack += OnAttackAnimationEvent;
        if (IsRanged)
        {
            _animator.SetLayerWeight(_animator.GetLayerIndex("Ranged"), 1);
            _animator.SetBool("IsRanged", true);
        }
    }

    public void PrepareForFight()
    {
        _animator.SetFloat("Speed", Random.Range(0.5f, 1.25f));
        _animator.SetFloat("AttackSpeed", IsRanged ? 2 : 1);
        _animator.SetFloat("Random", Random.Range(0, 3) / 2.0f);
        // if (!_isWalking)
        // {
        //     _animator.SetTrigger("StartCheering");
        // }
    }

    public void Move()
    {
        if (!_isWalking || IsDead)
            return;

        bool isTargetApproached = IsTargetNearby();
        if (isTargetApproached)
        {
            StopWalking();
            _hasReachedTarget = true;
        }
        else
        {
            float step = Speed * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, _approachTarget.position, step);
            _characterController.Move(newPosition - transform.position);
            // if (Vector3.Distance(transform.position, newPosition) >= step * 0.95f)
            // {
            //     StopWalking();
            // }
        }
    }

    private void StopWalking()
    {
        _animator.SetBool("IsWalking", false);
        _isWalking = false;
        PrepareAttack();
    }

    private void PrepareAttack()
    {
        _animator.SetTrigger("Prepare");
        _isPreparing = true;
        Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(_ => _isPreparing = false);
    }

    private bool IsTargetNearby(float extraDelta = 0)
    {
        return Vector3.Distance(transform.position, _approachTarget.position) <= _approachDistance + extraDelta;
    }

    public void Attack()
    {
        if (IsDead || _isWalking || _isPreparing || _isAttackOnCooldown || _enemy == null || _enemy.IsDead)
            return;

        LookAtTarget(_enemy.transform);
        _animator.SetTrigger("Attack");
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        _isAttackOnCooldown = true;
        yield return new WaitForSeconds(_attackCooldown);
        _isAttackOnCooldown = false;
    }

    public void SetMoveTarget(Transform target)
    {
        if (IsDead)
            return;

        _healthBar.gameObject.SetActive(true);
        _approachTarget = target;
        if (IsTargetNearby(0.1f))
        {
            PrepareAttack();
            _hasReachedTarget = true;
            _nextInQueue = null;
        }
        else
        {
            _animator.SetBool("IsWalking", true);
            _isWalking = true;
            _hasReachedTarget = false;
            onContinueQueue?.Invoke();
        }
        LookAtTarget(target);
    }

    private void LookAtTarget(Transform target)
    {
        _animator.transform.DOKill();
        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;
        _animator.transform.DOLookAt(targetPosition, 0.5f);
    }

    public void SetAttackTarget(NewSingleCharacter target)
    {
        _enemy = target;
        if (target != null)
        {
            SetMoveTarget(_enemy.transform);
            target.OnDeath += () => _fightController.UpdateTargetFor(this);
        }
    }

    private void OnAttackAnimationEvent()
    {
        if (_enemy == null || _isWalking)
            return;

        if (IsRanged)
        {
            LaunchProjectile();
        }
        else
        {
            DealDamage();
        }
    }

    private void LaunchProjectile()
    {
        var thrownProjectile = Instantiate(projectile, projectileStartPos.position, Quaternion.identity);
        thrownProjectile.Initialize(_enemy, projectileSpeed);
        thrownProjectile.OnHit += DealDamage;
    }

    private void DealDamage()
    {
        _enemy.TakeDamage(DamageAmount, this);
        Vector3 position = IsRanged ? _enemy.transform.position + new Vector3(0, 0.2f, 0) : ParticleTransform.position;
        ParticlesManager.Instance.MakePopParticles(position, GirlLevel);
    }

    private void TakeDamage(float damage, NewSingleCharacter from)
    {
        if (IsEnemy)
        {
            int earnedMoney = (int) (damage * 130f + 0.5f);
            _moneyController.AddMoney(earnedMoney);
            MoneyEffectManager.Instance.MakeMoneyEffect(transform.position + Vector3.up * 0.25f, earnedMoney);
        }

        Health -= damage;
        if (Health < 0)
            Health = 0;
        _healthBar.value = Health;
        if (Health <= 0)
        {
            //_animator.SetTrigger("Die");
            RagdollThrow(from.transform);
            Death(from);
        }
        else
        {
            _animator.SetTrigger("Damage");
        }
        _fightController.DamageTaken(this);
        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        if (_isDamageFlashing)
            yield break;

        _isDamageFlashing = true;
        Material[] materials = GetComponentsInChildren<Renderer>().SelectMany(mesh => mesh.materials).ToArray();

        foreach (var material in materials)
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.white * 0.3f);
        }

        yield return new WaitForSeconds(0.1f);
        
        foreach (var material in materials)
        {
            material.DisableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.black);
        }

        _isDamageFlashing = false;
    }

    private void Death(NewSingleCharacter from)
    {
        _collider.enabled = false;
        // _rigidbody.isKinematic = true;
        // _rigidbody.velocity = Vector3.zero;
        _healthBar.gameObject.SetActive(false);
        //_animator.SetTrigger("Die");
        IsDead = true;
        _fightController.CharacterDied(this);
        OnDeath?.Invoke();
        onContinueQueue?.Invoke();
    }

    public void Dance()
    {
        if (!IsDead)
        {
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("Dance", true);
            _animator.SetLayerWeight(_animator.GetLayerIndex("Ranged"), 0);
        }
    }

    private void RagdollThrow(Transform explosionCenter)
    {
        ToggleRagdoll(true);
        Hips.AddForce((transform.position - explosionCenter.position + Vector3.up).normalized * _ragdollThrowPower);
    }

    private void ToggleRagdoll(bool active)
    {
        _animator.enabled = !active;

        foreach (Rigidbody rb in _ragdollRigidbodies)
        {
            rb.isKinematic = !active;
        }

        foreach (Collider col in _ragdollColliders)
        {
            col.enabled = active;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (HasReachedTargetQueue() || !hit.gameObject.CompareTag("Character"))
            return;

        var otherCharacter = hit.gameObject.GetComponent<NewSingleCharacter>();

        if (otherCharacter.IsEnemy != IsEnemy)
        {
            SetAttackTarget(_enemy);
        }
        else
        {
            if (otherCharacter.HasReachedTargetQueue())
            {
                _nextInQueue = otherCharacter;
                _nextInQueue.onContinueQueueObservable.First().Subscribe(_ =>
                {
                    _nextInQueue = null;
                    _fightController.UpdateTargetFor(this);
                    onContinueQueue?.Invoke();
                });
                StopWalking();
            }
        }
    }

    private const int MAX_QUEUE_DEPTH = 10;
    private bool HasReachedTargetQueue(int depth = 0)
    {
        return _hasReachedTarget || depth < MAX_QUEUE_DEPTH && _nextInQueue != null &&  _nextInQueue.HasReachedTargetQueue(depth + 1);
    }
}