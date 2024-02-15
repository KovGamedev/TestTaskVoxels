using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class NewGirlsLevelUpController : MonoBehaviour
{
    public GameObject[] Girls;
    public GameObject[] GirlsRanged;
    public NewSinglePlace[] _places;

    private TutorialController _tutorialController;
    private string _isFirst = "isdfwwy";

    private void Awake()
    {
        _tutorialController = GetComponent<TutorialController>();
    }

    private void Start()
    {
        //_places = FindObjectsOfType<NewSinglePlace>();
        if (PlayerPrefs.GetInt(_isFirst) == 0)
        {
            PlayerPrefs.SetInt(_isFirst, 1);
            //AddGirl();
        }
    }

    public NewSingleCharacter[] GetUnlockedGirls()
    {
        var activeAllies = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Character))
            .Select(girl => girl.GetComponent<NewSingleCharacter>())
            .OrderBy(girl => girl.GirlLevel)
            .ToArray();

        var maxMelee = activeAllies.LastOrDefault(girl => !girl.IsRanged);
        var maxRanged = activeAllies.LastOrDefault(girl => girl.IsRanged);

        List<GameObject> result = new List<GameObject>();
        if (maxMelee != null)
        {
            result.AddRange(Girls.Take(maxMelee.GirlLevel + 1));
        }
        if (maxRanged != null)
        {
            result.AddRange(GirlsRanged.Take(maxRanged.GirlLevel + 1));
        }

        return result.Select(obj => obj.GetComponent<NewSingleCharacter>())
            .OrderBy(girl => girl.GirlLevel + (girl.IsRanged ? 0.5f : 0f)).ToArray();
    }

    public void LevelUpGirl(SingleDragableCharacter character)
    {
        var nextLevelGirls = character.IsRanged ? GirlsRanged : Girls;

        if (character.GirlLevel + 1 >= nextLevelGirls.Length)
            return;

        _tutorialController.CharacterUpgraded();
        GameObject girlPrefab = nextLevelGirls[character.GirlLevel + 1];
        ParticlesManager.Instance.MakeSmokeParticles(character.transform.position, girlPrefab.GetComponent<NewSingleCharacter>().SpawnColor);
        GameObject newGirl = Instantiate(girlPrefab, character.transform.position, character.transform.rotation);
        newGirl.GetComponentInChildren<Animator>().SetTrigger("Spin");
        newGirl.transform.DOScale(1.25f, 0.5f).SetLoops(2, LoopType.Yoyo);
        Destroy(character.gameObject);
    }

    public bool CanAddGirl()
    {
        return _places.Any(place => !place.IsTaken);
    }

    public void AddGirl(GirlType girlType)
    {
        GameObject[] girls = girlType switch
        {
            GirlType.Melee => Girls,
            GirlType.Ranged => GirlsRanged,
            _ => throw new InvalidEnumArgumentException()
        };

        NewSinglePlace place = _places.First(place => !place.IsTaken);
        GameObject girlPrefab = girls[0];
        ParticlesManager.Instance.MakeSmokeParticles(place.GetPlaceSitPosition(), girlPrefab.GetComponent<NewSingleCharacter>().SpawnColor);
        Instantiate(girlPrefab, place.GetPlaceSitPosition(), Quaternion.Euler(Vector3.up * 90f));
    }

    public void AddGirl(NewSinglePlace place)
    {
        if (!place.IsTaken)
        {
            Instantiate(Girls[0], place.GetPlaceSitPosition(), Quaternion.Euler(Vector3.up * 90f));
        }
    }

    public void PlaceGirl(NewSinglePlace place, int girlData)
    {
        if (girlData == -1)
            return;

        bool isRanged = (girlData & 1) > 0;
        int level = girlData >> 1;
        var girls = isRanged ? GirlsRanged : Girls;
        Instantiate(girls[level], place.GetPlaceSitPosition(), Quaternion.Euler(Vector3.up * 90f));
    }
}