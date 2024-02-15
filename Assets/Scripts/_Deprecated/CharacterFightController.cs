using UnityEngine;

public class CharacterFightController : MonoBehaviour
{
	private SingleCharacter[] _characters;
	private SingleEnemyCharacter[] _enemies;
	private MainGameController _mainGameController;
	private bool _needToFight;

	private void Start()
	{
		_enemies = FindObjectsOfType<SingleEnemyCharacter>();
		_mainGameController = GetComponent<MainGameController>();
	}
	private void FixedUpdate()
	{
		if (_needToFight)
		{
			for (int i = 0; i < _characters.Length; i++)
			{
				_characters[i].MoveCharacter();
			}
			for (int i = 0; i < _enemies.Length; i++)
			{
				_enemies[i].MoveCharacter();
			}
		}
		if (Input.GetKey(KeyCode.F))
		{
			StartFighting();
		}
	}
	public void StartFighting()
	{
		_characters = FindObjectsOfType<SingleCharacter>();
		for (int i = 0; i < _characters.Length; i++)
		{
			_characters[i].PrepareForMovement();
		}
		for (int i = 0; i < _enemies.Length; i++)
		{
			_enemies[i].PrepareForMovement();
		}
		_needToFight = true;
	}
	public SingleEnemyCharacter GetEnemyCharacter(Vector3 position)
	{
		float minDistance = 1000f;
		bool findEnemy = false;
		SingleEnemyCharacter enemy = _enemies[0];
		for (int i = 0; i < _enemies.Length; i++)
		{
			if(!_enemies[i].IsDead && Vector3.Distance(_enemies[i].transform.position, position) < minDistance )
			{
				findEnemy = true;
				minDistance = Vector3.Distance(_enemies[i].transform.position, position);
				enemy = _enemies[i];
			}
		}
		if (!findEnemy)
		{
			_needToFight = false;
			_mainGameController.LevelWin(); 
			PlayerDance();
			Debug.Log("LevelWin");
		}
		return enemy;
	}
	public SingleCharacter GetPlayerCharacter(Vector3 position)
	{
		float minDistance = 1000f;
		bool findcharacter = false;
		SingleCharacter character = _characters[0];
		for (int i = 0; i < _characters.Length; i++)
		{
			if(!_characters[i].IsDead && Vector3.Distance(_characters[i].transform.position, position) < minDistance)
			{
				findcharacter = true;
				minDistance = Vector3.Distance(_characters[i].transform.position, position);
				character = _characters[i];
			}
		}
		if (!findcharacter)
		{
			_needToFight = false;
			_mainGameController.LevelLose();
			EnemyDance();
			Debug.Log("LevelLose");
		}
		return character;
	}
	private void EnemyDance()
	{
		for (int i = 0; i < _enemies.Length; i++)
		{
			_enemies[i].Dance();
		}
	}
	private void PlayerDance()
	{
		for (int i = 0; i < _characters.Length; i++)
		{
			_characters[i].Dance();
		}
	}
}
