using UnityEngine;

public class TutorialController : MonoBehaviour
{
	public GameObject BuyButton;
	public GameObject FightButton;
	public GameObject[] FirstPhaseObjects;
	public GameObject[] SecondPhaseObjects;
	public GameObject[] ThirdPhaseObjects;

	private NewCameraRaycastController _raycastController;
	private NewGirlsLevelUpController _girlsController;


	private string _firstPhaseComplete = "f32wsdaxcdsd";
	private string _secondPhaseComplete = "wdsdsqqqd";
	private string _thirdPhaseComplete = "wdsddddddddsqqqd";
	private string _isFirst = "wdsdsqqqqqqd";

	private bool _isFirstPhaseComplete;
	private bool _isSecondPhaseComplete;
	private bool _isThirdPhaseComplete;
	private void Awake()
	{
		_raycastController = GetComponent<NewCameraRaycastController>();
		_girlsController = GetComponent<NewGirlsLevelUpController>();
		if (PlayerPrefs.GetInt(_firstPhaseComplete) == 1)
			_isFirstPhaseComplete = true;
		if (PlayerPrefs.GetInt(_secondPhaseComplete) == 1)
			_isSecondPhaseComplete = true;
		if (PlayerPrefs.GetInt(_thirdPhaseComplete) == 1)
			_isThirdPhaseComplete = true;
		for (int i = 0; i < FirstPhaseObjects.Length; i++)
		{
			FirstPhaseObjects[i].SetActive(false);
		}
		for (int i = 0; i < SecondPhaseObjects.Length; i++)
		{
			SecondPhaseObjects[i].SetActive(false);
		}
		for (int i = 0; i < ThirdPhaseObjects.Length; i++)
		{
			ThirdPhaseObjects[i].SetActive(false);
		}
	}
	public void StartTutorial()
	{
		if (_isFirstPhaseComplete)
		{
			if (_isSecondPhaseComplete)
			{
				return;
			}
			else
			{
				ActivateSecondPart();
			}
		}
		else
		{
			ActivateFirstPart();
		}
	}
	public void ActivateFirstPart()
	{
		if (PlayerPrefs.GetInt(_isFirst) != 1)
		{
			PlayerPrefs.SetInt(_isFirst, 1);
			_girlsController.AddGirl(GirlType.Melee);
		}
		for (int i = 0; i < FirstPhaseObjects.Length; i++)
		{
			FirstPhaseObjects[i].SetActive(true);
		}
		FightButton.SetActive(false);
		_raycastController.ActivateController(false);
		//включаем нужные обьекты
	}
	public void ActivateSecondPart()
	{
		for (int i = 0; i < FirstPhaseObjects.Length; i++)
		{
			FirstPhaseObjects[i].SetActive(false);
		}
		for (int i = 0; i < SecondPhaseObjects.Length; i++)
		{
			SecondPhaseObjects[i].SetActive(true);
		}
		_raycastController.ActivateController(true);
		FightButton.SetActive(false);
		//включаем эффект перетаскивания
	}
	public void ActivateThirdPart()
	{
		for (int i = 0; i < FirstPhaseObjects.Length; i++)
		{
			FirstPhaseObjects[i].SetActive(false);
		}
		for (int i = 0; i < SecondPhaseObjects.Length; i++)
		{
			SecondPhaseObjects[i].SetActive(false);
		}
		for (int i = 0; i < ThirdPhaseObjects.Length; i++)
		{
			ThirdPhaseObjects[i].SetActive(true);
		}
		_raycastController.ActivateController(true);
		FightButton.SetActive(true);
		//включаем эффект перетаскивания
	}
	public void EndTutorial()
	{
		for (int i = 0; i < FirstPhaseObjects.Length; i++)
		{
			FirstPhaseObjects[i].SetActive(false);
		}
		for (int i = 0; i < SecondPhaseObjects.Length; i++)
		{
			SecondPhaseObjects[i].SetActive(false);
		}
		for (int i = 0; i < ThirdPhaseObjects.Length; i++)
		{
			ThirdPhaseObjects[i].SetActive(false);
		}
	}
	public void CharacterBought()
	{
		if (!_isFirstPhaseComplete)
		{
			_isFirstPhaseComplete = true;
			PlayerPrefs.SetInt(_firstPhaseComplete, 1);
			ActivateSecondPart();
		}
	}
	public void CharacterUpgraded()
	{
		if (!_isSecondPhaseComplete)
		{
			_isSecondPhaseComplete = true;
			PlayerPrefs.SetInt(_secondPhaseComplete, 1);
			ActivateThirdPart();
		}
	}
	public void StartFight()
	{
		if (!_isThirdPhaseComplete)
		{
			_isThirdPhaseComplete = true;
			PlayerPrefs.SetInt(_thirdPhaseComplete, 1);
			EndTutorial();
		}
	}

}