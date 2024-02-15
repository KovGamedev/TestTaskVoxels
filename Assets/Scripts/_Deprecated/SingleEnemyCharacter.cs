using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SingleEnemyCharacter : MonoBehaviour
{
	public Material WhiteMaterial;
	public float Speed;
	public float PunchSpeed;
	public float Health;
	public float DamageAmount;
	public float MinDistance;

	[HideInInspector] public bool IsDead;

	private Rigidbody _rigidbody;
	private Collider _collider;
	private Slider _healthBar;
	private Canvas _characterCanvas;
	private Animator _animator;
	private List<Material[]> _girlMaterials;
	private Renderer[] _renderers;
	private CharacterFightController _characterController;
	private SingleCharacter _goalCharacter;
	private MoneyController _moneyController;
	[SerializeField]private Transform _modelTransform;
	private Vector3 _velocityVector;
	private Vector3 _punchPosition;
	private Vector3 _startPunchPosition;
	private bool HasGoal;
	private bool _isAttacking;
	private bool _isWalking;
	private bool _isWin;
	private bool _needToMoveToPunch;
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
		_characterController = FindObjectOfType<CharacterFightController>();
		_moneyController = FindObjectOfType<MoneyController>();
		_animator = GetComponentInChildren<Animator>();
		_renderers = GetComponentsInChildren<Renderer>();
		_healthBar = GetComponentInChildren<Slider>();
		_characterCanvas = GetComponentInChildren<Canvas>();
		//_modelTransform = _animator.transform.parent;
		_modelTransform = transform;

		_rigidbody.isKinematic = true;
		_healthBar.maxValue = Health;
		_healthBar.value = Health;
		_healthBar.gameObject.SetActive(false);
		_girlMaterials = new List<Material[]>();
		_velocityVector = new Vector3();
		for (int i = 0; i < _renderers.Length; i++)
		{
			_girlMaterials.Add(_renderers[i].sharedMaterials);
		}
	}
	public void PrepareForMovement()
	{
		_healthBar.gameObject.SetActive(true);
		_rigidbody.isKinematic = false;
	}
	public void MoveCharacter()
	{
		if (!IsDead && !_isWin)
		{
			if (HasGoal)
			{
				_characterCanvas.transform.LookAt(Camera.main.transform);
				if (_goalCharacter.IsDead)
				{
					_isAttacking = false;
					_animator.SetBool("IsAttacking", false);
					HasGoal = false;
				}
				else
				{
					if (Vector3.Distance(transform.position, _goalCharacter.transform.position) <= MinDistance)
					{
						_rigidbody.velocity = Vector3.zero;
						if (!_isAttacking)
						{
							_rigidbody.isKinematic = true;
							if (_isWalking)
							{
								_isWalking = false;
								_animator.SetBool("IsWalking", false);
							}
							_isAttacking = true;
							_animator.SetBool("IsAttacking", true);
							_animator.SetTrigger("Punch");
							Attack();
						}
					}
					else
					{
						_rigidbody.isKinematic = false;
						//transform.position = Vector3.MoveTowards(transform.position, _goalCharacter.transform.position, Speed);
						_velocityVector = (_goalCharacter.transform.position - transform.position).normalized;
						_velocityVector.y = 0;
						_rigidbody.velocity = _velocityVector * (Speed * 80f);
						transform.LookAt(_goalCharacter.transform.position);
					}
				}
			}
			else
			{
				if (!_isWalking)
				{
					_isWalking = true;
					_animator.SetBool("IsWalking", true);
				}
				_goalCharacter = _characterController.GetPlayerCharacter(transform.position);
				HasGoal = true;
				_isAttacking = false;
			}
			if (_needToMoveToPunch)
			{
				_modelTransform.position = Vector3.MoveTowards(_modelTransform.position, _punchPosition, PunchSpeed);
				if(_modelTransform.position == _punchPosition)
				{
					_needToMoveToPunch = false;
					//_punchPosition = _startPunchPosition;
				}
				if(_modelTransform.position == _startPunchPosition)
				{
					_punchPosition = _startPunchPosition;
					_needToMoveToPunch = false;
				}
			}
		}
	}
	private void Attack()
	{
		if (!IsDead && _isAttacking)
		{
			SendDamage();
			Invoke(nameof(Attack), 0.5f);
		}
	}
	private void SendDamage()
	{
		_goalCharacter.TakeDamage(DamageAmount, transform);
	}
	public void TakeDamage(float amount, Transform enemy)
	{
		Health -= amount;
		_healthBar.value = Health;
		_moneyController.AddMoney((int)(amount * 10f + 0.5f));
		MoneyEffectManager.Instance.MakeMoneyEffect(transform.position + Vector3.up * 0.25f, (int)(amount * 10f + 0.5f));
		if (Health <= 0)
		{
			KillCharacter();
		}
		else
		{
			MakePunch(enemy);
			//SetGirlWhite();
			Invoke(nameof(SetDefaultMaterials), 0.2f);
		}
	}
	private void MakePunch(Transform enemy)
	{
		_needToMoveToPunch = false;
		//_modelTransform.localPosition = Vector3.zero;
		_startPunchPosition = _modelTransform.position;
		_punchPosition = _modelTransform.position + (_modelTransform.position - enemy.position).normalized * 0.1f;
		_punchPosition.y = _modelTransform.position.y;
		_needToMoveToPunch = true;
	}
	private void SetGirlWhite()
	{
		for (int i = 0; i < _renderers.Length; i++)
		{
			Material[] temp = new Material[_renderers[i].sharedMaterials.Length];
			for (int j = 0; j < temp.Length; j++)
			{
				temp[j] = WhiteMaterial;
			}
			_renderers[i].sharedMaterials = temp;
		}
	}
	private void SetDefaultMaterials()
	{
		for (int i = 0; i < _renderers.Length; i++)
		{
			_renderers[i].sharedMaterials = _girlMaterials[i];
		}
	}
	public void KillCharacter()
	{
		_collider.enabled = false;
		_rigidbody.isKinematic = true;
		_rigidbody.velocity = Vector3.zero;
		_healthBar.gameObject.SetActive(false);
		_animator.SetTrigger("Die");
		IsDead = true;
	}
	public void Dance()
	{
		_rigidbody.isKinematic = true;
		_rigidbody.velocity = Vector3.zero;
		_healthBar.gameObject.SetActive(false);
		_isWin = true;
		_animator.SetTrigger("Dance");
	}
}
