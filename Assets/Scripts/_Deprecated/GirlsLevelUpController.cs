using UnityEngine;
public class GirlsLevelUpController : MonoBehaviour
{
	public GameObject[] Girls;
	public SinglePlace[] _places;

	private string _isFirst = "isFirsew";

	private void Start()
	{
		_places = FindObjectsOfType<SinglePlace>();
		if(PlayerPrefs.GetInt(_isFirst) == 0)
		{
			PlayerPrefs.SetInt(_isFirst, 1);
			AddGirl();
		}
	}
	public void LevelUpGirl(SingleCharacter character)
	{
		ParticlesManager.Instance.MakeSmokeParticles(character.transform.position + Vector3.up * 0.25f);
		Instantiate(Girls[character.GirlLevel + 1], character.transform.position, character.transform.rotation);
		Destroy(character.gameObject);
	}
	public bool CanAddGirl()
	{
		for (int i = 0; i < _places.Length; i++)
		{
			if (!_places[i].IsBusy())
			{
				return true;
			}
		}

		return false;
	}
	public void AddGirl()
	{
		for (int i = 0; i < _places.Length; i++)
		{
			if (!_places[i].IsBusy())
			{
				ParticlesManager.Instance.MakeSmokeParticles(_places[i].GetPlaceSitPosition() + Vector3.up * 0.25f);
				Instantiate(Girls[0], _places[i].GetPlaceSitPosition(), Quaternion.Euler(Vector3.up * 90f));
				break;
			}
		}
	}
	public void PlaceGirl(SinglePlace place, int girlNumber)
	{
		if (girlNumber > 0)
		{
			Instantiate(Girls[girlNumber - 1], place.GetPlaceSitPosition(), Quaternion.Euler(Vector3.up * 90f));
		}
	}
}