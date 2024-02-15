using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
	public static ParticlesManager Instance;

	public ParticleSystem[] ConfettiParticles;

	public ParticleSystem SmokeParticles;
	private ParticleSystem[] _smokeParticlesPool;
	[SerializeField] private bool smokeGirlColor;
	[SerializeField] private Vector3 smokeOffset;

	public ParticleSystem AttackPopParticles0;
	private ParticleSystem[] _attackPopParticles0;
	public ParticleSystem AttackPopParticles1;
	private ParticleSystem[] _attackPopParticles1;
	public ParticleSystem AttackPopParticles2;
	private ParticleSystem[] _attackPopParticles2;
	public ParticleSystem AttackPopParticles3;
	private ParticleSystem[] _attackPopParticles3;
	public ParticleSystem AttackPopParticles4;
	private ParticleSystem[] _attackPopParticles4;
	public ParticleSystem AttackPopParticles5;
	private ParticleSystem[] _attackPopParticles5;


	private ParticleSystem[] _temp;

	private Vector3 _poolPositionVector = new Vector3(0, -10, -20f);
	private Vector3 _poolRotationVector = new Vector3(-90f, 0, 0);

	private void Awake()
	{
		Instance = this;

		_smokeParticlesPool = new ParticleSystem[15];
		for (int i = 0; i < _smokeParticlesPool.Length; i++)
		{
			_smokeParticlesPool[i] = Instantiate(SmokeParticles);
			_smokeParticlesPool[i].Stop();
		}

		_attackPopParticles0 = new ParticleSystem[2];
		for (int i = 0; i < _attackPopParticles0.Length; i++)
		{
			_attackPopParticles0[i] = Instantiate(AttackPopParticles0, _poolPositionVector, Quaternion.Euler(_poolRotationVector));
			_attackPopParticles0[i].Stop();
		}
		_attackPopParticles1 = new ParticleSystem[2];
		for (int i = 0; i < _attackPopParticles1.Length; i++)
		{
			_attackPopParticles1[i] = Instantiate(AttackPopParticles1, _poolPositionVector, Quaternion.Euler(_poolRotationVector));
			_attackPopParticles1[i].Stop();
		}
		_attackPopParticles2 = new ParticleSystem[2];
		for (int i = 0; i < _attackPopParticles2.Length; i++)
		{
			_attackPopParticles2[i] = Instantiate(AttackPopParticles2, _poolPositionVector, Quaternion.Euler(_poolRotationVector));
			_attackPopParticles2[i].Stop();
		}
		_attackPopParticles3 = new ParticleSystem[2];
		for (int i = 0; i < _attackPopParticles3.Length; i++)
		{
			_attackPopParticles3[i] = Instantiate(AttackPopParticles3, _poolPositionVector, Quaternion.Euler(_poolRotationVector));
			_attackPopParticles3[i].Stop();
		}
		_attackPopParticles4 = new ParticleSystem[2];
		for (int i = 0; i < _attackPopParticles4.Length; i++)
		{
			_attackPopParticles4[i] = Instantiate(AttackPopParticles4, _poolPositionVector, Quaternion.Euler(_poolRotationVector));
			_attackPopParticles4[i].Stop();
		}
		_attackPopParticles5 = new ParticleSystem[2];
		for (int i = 0; i < _attackPopParticles5.Length; i++)
		{
			_attackPopParticles5[i] = Instantiate(AttackPopParticles5, _poolPositionVector, Quaternion.Euler(_poolRotationVector));
			_attackPopParticles5[i].Stop();
		}

		//_сonfettiParticles = Instantiate(ConfettiParticles, _poolPositionVector, Quaternion.Euler(_poolRotationVector));
		ConfettiParticles.ForEach(system => system.Stop());
	}

	public void MakeSmokeParticles(Vector3 position, Color color = default)
	{
		var smokeParticles = _smokeParticlesPool.FirstOrDefault(system => !system.isPlaying) ?? _smokeParticlesPool.First();
		if (smokeGirlColor)
		{
			foreach (var effects in smokeParticles.GetComponentsInChildren<ParticleSystem>())
			{
				effects.startColor = color;
			}
		}

		smokeParticles.transform.position = position + smokeOffset;
		smokeParticles.Play();
	}

	public void MakePopParticles(Vector3 position, int level = 0)
	{
		switch (level)
		{
			case 0:
				_temp = _attackPopParticles0;
				break;
			case 1:
				_temp = _attackPopParticles1;
				break;
			case 2:
				_temp = _attackPopParticles2;
				break;
			case 3:
				_temp = _attackPopParticles3;
				break;
			case 4:
				_temp = _attackPopParticles4;
				break;
			case 5:
				_temp = _attackPopParticles5;
				break;
			default:
				goto case 5;
		}
		for (int i = 0; i < _temp.Length; i++)
		{
			if (!_temp[i].isPlaying || i == _temp.Length - 1)
			{
				_temp[i].transform.position = position;
				_temp[i].Play();
				break;
			}
		}
	}

	public void MakeConfettiParticles()
	{
		ConfettiParticles.ForEach(system => system.Play());
	}
}