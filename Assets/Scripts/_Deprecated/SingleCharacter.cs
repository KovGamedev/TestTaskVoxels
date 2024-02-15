using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SingleCharacter : MonoBehaviour
{
	public Material WhiteMaterial;
	public float Speed;
	public float PunchSpeed;
	public float Health;
	public float DamageAmount;
	public float MinDistance;
	public int PlaceLayer;
	public int GirlLevel;

	[HideInInspector] public bool IsDead;

	private Ray _ray;
	private RaycastHit _hit;
	private List<Material[]> _girlMaterials;
	[SerializeField] private Renderer[] _renderers;
	private Rigidbody _rigidbody;
	private Collider _collider;
	private Animator _animator;
	private SinglePlace _sitingPlace;
	private SinglePlace _selectedPlace;
	private SinglePlace _tempSelectedPlace;
	private SingleEnemyCharacter _goalCharacter;
	private CharacterFightController _characterController;
	private Slider _healthBar;
	private Canvas _characterCanvas;
	private Transform _modelTransform;
	private Vector3 _velocityVector;
	private Vector3 _characterPosition;
	private Vector3 _calculatedPosition;
	private Vector3 _startInput;
	private Vector3 _startPositionVector;
	private Vector3 _punchPosition;
	private Vector3 _startPunchPosition;
	private bool _characterPickedUp;
	private bool _newPlaceSelected;
	private bool _startCalculated;
	private bool _canUpgrade;
	private bool _hasGoal;
	private bool _isAttacking;
	private bool _isWalking;
	private bool _isWin;
	private bool _needToMoveToPunch;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
		_characterController = FindObjectOfType<CharacterFightController>();
		_renderers = GetComponentsInChildren<Renderer>();
		_characterCanvas = GetComponentInChildren<Canvas>();
		_healthBar = GetComponentInChildren<Slider>();
		_animator = GetComponentInChildren<Animator>();
		//_modelTransform = _animator.transform.parent;
		_modelTransform = transform;

		_velocityVector = new Vector3();
		_calculatedPosition = new Vector3();
		_rigidbody.isKinematic = true;
		_healthBar.maxValue = Health;
		_healthBar.value = Health;
		_healthBar.gameObject.SetActive(false);
		_girlMaterials = new List<Material[]>();
		for (int i = 0; i < _renderers.Length; i++)
		{
			_girlMaterials.Add(_renderers[i].sharedMaterials);
		}
		_sitingPlace = GetFloorDown();
		_characterPickedUp = true;
		PlaceCharacter();
		//SetGirlWhite();
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
			if (_hasGoal)
			{
				_characterCanvas.transform.LookAt(Camera.main.transform);
				if (_goalCharacter.IsDead)
				{
					_isAttacking = false;
					_animator.SetBool("IsAttacking", false);
					_hasGoal = false;
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
				_goalCharacter = _characterController.GetEnemyCharacter(transform.position);
				_hasGoal = true;
				_isAttacking = false;
			}
			if (_needToMoveToPunch)
			{
				_modelTransform.position = Vector3.MoveTowards(_modelTransform.position, _punchPosition, PunchSpeed);
				if (_modelTransform.position == _punchPosition)
				{
					_needToMoveToPunch = false;
					//_punchPosition = _startPunchPosition;
				}
				if (_modelTransform.position == _startPunchPosition)
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
		if (Health <= 0)
		{
			KillCharacter();
		}
		else
		{
			//SetGirlWhite();
			Invoke(nameof(SetDefaultMaterials), 0.2f);
			MakePunch(enemy);
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
	private SinglePlace GetFloorDown()
	{
		_ray = new Ray(transform.position + Vector3.up * 0.2f, Vector3.down);
		if (Physics.Raycast(_ray, out _hit, PlaceLayer) && _hit.transform.CompareTag(TagManager.GetTag(TagType.SinglePlace)))
		{
			return _hit.transform.GetComponent<SinglePlace>();
		}
		return null;
	}
	public void PickUpCharacter()
	{
		try
		{
			_startCalculated = false;
			_characterPickedUp = true;
			_sitingPlace.FreePlace();
			_characterPosition = transform.position;
			_characterPosition.y = 4.4f;
			transform.position = _characterPosition;
			_animator.SetBool("IsFloating", true);
		}
		catch { }
	}
	public void DragCharacter(Vector3 inputPosition)
	{
		if (_characterPickedUp)
		{
			if (!_startCalculated)
			{
				_startCalculated = true;
				_startInput = inputPosition;
				_startPositionVector = transform.position;
			}
			_calculatedPosition.z = _startInput.x - inputPosition.x + _startPositionVector.z;
			_calculatedPosition.x = inputPosition.y - _startInput.y + _startPositionVector.x;
			_calculatedPosition.y = transform.position.y;
			transform.position = _calculatedPosition;
		}
		_tempSelectedPlace = GetFloorDown();
		//_tempSelectedPlace = null;
		if (_tempSelectedPlace != null)
		{
			if (!_tempSelectedPlace.IsBusy())
			{
				_newPlaceSelected = true;
				if (_selectedPlace != null)
					_selectedPlace.SelectPlace(false);
				_selectedPlace = _tempSelectedPlace;
				_selectedPlace.SelectPlace(true);
			}
			else
			{
				if (_tempSelectedPlace.CanMerge(this))
				{
					_canUpgrade = true;
					_newPlaceSelected = true;
					if (_selectedPlace != null)
						_selectedPlace.SelectPlace(false);
					_selectedPlace = _tempSelectedPlace;
					_selectedPlace.SelectPlace(true);
				}
			}
		}
		else
		{
			_selectedPlace.SelectPlace(false);
			_newPlaceSelected = false;
			_canUpgrade = false;
		}
	}
	public void PlaceCharacter()
	{
		if (_characterPickedUp)
		{
			_animator.SetBool("IsFloating", false);
			if (_canUpgrade)
			{
				_selectedPlace.UpgradeCharacter(this);
			}
			else
			{
				_characterPickedUp = false;
				if (_newPlaceSelected)
				{
					transform.position = _selectedPlace.GetPlaceSitPosition();
					_selectedPlace.TakePlace(this);
					_selectedPlace.SelectPlace(false);
				}
				else
				{
					transform.position = _sitingPlace.GetPlaceSitPosition();
					_sitingPlace.TakePlace(this);
					_sitingPlace.SelectPlace(false);
				}
			}
		}
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
