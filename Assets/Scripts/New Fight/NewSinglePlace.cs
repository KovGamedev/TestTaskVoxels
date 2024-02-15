using Sirenix.OdinInspector;
using UnityEngine;

public class NewSinglePlace : MonoBehaviour
{
    public Transform CharacterPlace;

    private NewGirlsLevelUpController _levelUpController;
    private SingleDragableCharacter _characterOnPlace;
    private SpriteRenderer _selectSprite;
    private string _placeSave = "plqwddddawcwd2y";
    [ShowInInspector, ReadOnly] public bool IsTaken { get; private set; }
    private bool _isSelected;

    private void Awake()
    {
        _placeSave = _placeSave + gameObject.name;
        _selectSprite = GetComponentInChildren<SpriteRenderer>();
        _levelUpController = FindObjectOfType<NewGirlsLevelUpController>();
        _selectSprite.enabled = false;
        int girlData = PlayerPrefs.GetInt(_placeSave, -1);
        if (girlData >= 0)
            _levelUpController.PlaceGirl(this, girlData);
    }

    public void FreePlace()
    {
        IsTaken = false;
        SaveData();
    }

    public bool CanMerge(SingleDragableCharacter character)
    {
        bool sameLevel = _characterOnPlace.GirlLevel == character.GirlLevel;
        bool sameClass = _characterOnPlace.IsRanged == character.IsRanged;
        bool notMaxLevel;
        if (!character.IsRanged)
        {
            notMaxLevel = _levelUpController.Girls.Length > character.GirlLevel + 1;
        }
        else
        {
            notMaxLevel = _levelUpController.GirlsRanged.Length > character.GirlLevel + 1;
        }
        return sameLevel && sameClass && notMaxLevel;
    }

    public void TakePlace(SingleDragableCharacter character)
    {
        _characterOnPlace = character;
        IsTaken = true;
        SaveData();
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

    public void UpgradeCharacter(SingleDragableCharacter character)
    {
        FreePlace();
        _levelUpController.LevelUpGirl(_characterOnPlace);
        Destroy(character.gameObject);
    }

    private void SaveData()
    {
        if (IsTaken)
        {
            int rangedFlag = _characterOnPlace.IsRanged ? 1 : 0;
            int saveData = (_characterOnPlace.GirlLevel << 1) | rangedFlag;
            PlayerPrefs.SetInt(_placeSave, saveData);
        }
        else
        {
            PlayerPrefs.SetInt(_placeSave, -1);
        }
        PlayerPrefs.Save();
    }
}