using UnityEngine;

public class SinglePlace : MonoBehaviour
{
	public Transform CharacterPlace;

	private GirlsLevelUpController _levelUpController;
	private SingleCharacter _characterOnPlace;
	private SpriteRenderer _selectSprite;
	private string _placeSave = "plqwaqq";
	private bool _isBusy;
	private bool _isSelected;
	private void Awake()
	{
		_placeSave = _placeSave + gameObject.name;
		_selectSprite = GetComponentInChildren<SpriteRenderer>();
		_levelUpController = FindObjectOfType<GirlsLevelUpController>();
		_selectSprite.enabled = false;
		_levelUpController.PlaceGirl(this, PlayerPrefs.GetInt(_placeSave));
	}
	private void Start()
	{
	}
	public bool IsBusy()
	{
		return _isBusy;
	}
	public void FreePlace()
	{
		_isBusy = false; 
		SavePlace();
	}
	public bool CanMerge(SingleCharacter character)
	{
		if (_characterOnPlace.GirlLevel == character.GirlLevel)
		{
			return true;
		}
		return false;
	}
	public void TakePlace(SingleCharacter character)
	{
		_characterOnPlace = character;
		_isBusy = true;
		SavePlace();
	}
	public void SavePlace()
	{
		if (_isBusy)
		{
			PlayerPrefs.SetInt(_placeSave, _characterOnPlace.GirlLevel + 1);
		}
		else
		{
			PlayerPrefs.SetInt(_placeSave, 0);
		}
	}
	public Vector3 GetPlaceSitPosition()
	{
		return CharacterPlace.position;
	}
	public void SelectPlace(bool needToSelect)
	{
		if (needToSelect)
		{
			if (!_isSelected)
			{
				_isSelected = true;
				_selectSprite.enabled = true;
			}
		}
		else
		{
			if (_isSelected)
			{
				_isSelected = false;
				_selectSprite.enabled = false;
			}
		}
	}
	public void UpgradeCharacter(SingleCharacter character)
	{
		FreePlace();
		_levelUpController.LevelUpGirl(_characterOnPlace);
		Destroy(character.gameObject);
	}
}