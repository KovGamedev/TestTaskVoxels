using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public interface IFightController
{
    event Action<float> OnAllyHealthInit;
    event Action<float> OnAllyHealthUpdate;
    event Action<float> OnEnemyHealthInit;
    event Action<float> OnEnemyHealthUpdate;
}

public class NewFightController : MonoBehaviour, IFightController
{
    public Transform EnemySpot;
    public Transform PlayerSpot;

    public event Action<float> OnAllyHealthInit;
    public event Action<float> OnAllyHealthUpdate;
    public event Action<float> OnEnemyHealthInit;
    public event Action<float> OnEnemyHealthUpdate;

    [ShowInInspector] private NewSingleCharacter[] _allies;
    [ShowInInspector] private NewSingleCharacter[] _enemies;

    private MainGameController _mainGameController;
    private TutorialController _tutorialController;
    private bool _needToFight;

    private void Awake()
    {
        _mainGameController = GetComponent<MainGameController>();
        _tutorialController = GetComponent<TutorialController>();
    }

    private void FixedUpdate()
    {
        if (_needToFight)
        {
            MoveCharacters();
            AttackCharacters();
        }

        if (Input.GetKey(KeyCode.F))
        {
            StartFighting();
        }
    }

    private void MoveCharacters()
    {
        foreach (NewSingleCharacter character in _allies.Union(_enemies))
        {
            character.Move();
        }
    }

    private void AttackCharacters()
    {
        foreach (NewSingleCharacter character in _allies.Union(_enemies))
        {
            character.Attack();
        }
    }

    public void DamageTaken(NewSingleCharacter character)
    {
        if (character.IsEnemy)
        {
            float enemiesHealth = _enemies.Select(ch => ch.Health).Where(hp => hp > 0).Sum();
            OnEnemyHealthUpdate?.Invoke(enemiesHealth);
        }
        else
        {
            float alliesHealth = _allies.Select(ch => ch.Health).Where(hp => hp > 0).Sum();
            OnAllyHealthUpdate?.Invoke(alliesHealth);
        }
    }

    public void CharacterDied(NewSingleCharacter character)
    {
        // NewSingleCharacter closestTarget = GetClosestTargetFor(character);
        // character.SetAttackTarget(closestTarget);
        
        if (IsFightOver(out bool win))
        {
            EndFight(win);
        }
    }

    public void StartFighting()
    {
        _tutorialController.StartFight();

        GameObject[] allies = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Character));
        _allies = allies.Select(go => go.GetComponent<NewSingleCharacter>()).ToArray();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.EnemyCharacter));
        _enemies = enemies.Select(go => go.GetComponent<NewSingleCharacter>()).ToArray();

        foreach (NewSingleCharacter character in _allies.Union(_enemies))
        {
            character.PrepareForFight();
            UpdateTargetFor(character);
        }

        _needToFight = true;
        
        OnAllyHealthInit?.Invoke(_allies.Sum(character => character.Health));
        OnEnemyHealthInit?.Invoke(_enemies.Sum(character => character.Health));
    }

    public void UpdateTargetFor(NewSingleCharacter character)
    {
        character.SetAttackTarget(GetClosestTargetFor(character));
    }

    private NewSingleCharacter GetClosestTargetFor(NewSingleCharacter character)
    {
        NewSingleCharacter[] targets = character.IsEnemy ? _allies : _enemies;
        // Vector3 position = isEnemy ? EnemySpot.position : PlayerSpot.position;
        Vector3 position = character.transform.position;

        NewSingleCharacter result = targets.Where(ch => !ch.IsDead)
            .OrderBy(ch => Vector3.Distance(ch.transform.position, position))
            .FirstOrDefault();

        return result;
    }

    private bool IsFightOver(out bool win)
    {
        win = _enemies.All(ch => ch.IsDead);
        bool lose = _allies.All(ch => ch.IsDead);
        return win || lose;
    }

    private void EndFight(bool win)
    {
        _needToFight = false;
        if (win)
            _mainGameController.LevelWin();
        else
            _mainGameController.LevelLose();
        CharactersDance(win);
    }

    private void CharactersDance(bool win)
    {
        NewSingleCharacter[] targets = win ? _allies : _enemies;
        foreach (NewSingleCharacter character in targets)
        {
            character.Dance();
        }
    }
}