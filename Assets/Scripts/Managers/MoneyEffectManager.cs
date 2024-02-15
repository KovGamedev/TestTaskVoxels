using UnityEngine;
using TMPro;

public class MoneyEffectManager : MonoBehaviour
{
	public static MoneyEffectManager Instance;

	public GameObject MoneyEffect;

	private GameObject[] _moneyEffects;
	private Animator[] _moneyEffectsAnimators;
	private TMP_Text[] _moneyEffectTexts;

	private Vector3 _poolPositionVector = new Vector3(0, -10, -20f);
	private Vector3 _poolRotationVector = new Vector3(0, 0, 0);

	private int _effectOrder;

	private void Awake()
	{
		Instance = this;
		_moneyEffects = new GameObject[30];
		_moneyEffectsAnimators = new Animator[30];
		_moneyEffectTexts = new TMP_Text[30];
		for (int i = 0; i < _moneyEffects.Length; i++)
		{
			_moneyEffects[i] = Instantiate(MoneyEffect, _poolPositionVector, Quaternion.Euler(_poolRotationVector));
			_moneyEffectsAnimators[i] = _moneyEffects[i].GetComponent<Animator>();
			_moneyEffectTexts[i] = _moneyEffects[i].GetComponentInChildren<TMP_Text>();
		}
	}
	public void MakeMoneyEffect(Vector3 position, int moneyAmount)
	{
		if(_effectOrder >= _moneyEffects.Length)
		{
			_effectOrder = 0;
		}
		_moneyEffects[_effectOrder].transform.position = position;
		_moneyEffectTexts[_effectOrder].text ="$ " + moneyAmount.ToString();
		_moneyEffectsAnimators[_effectOrder].SetTrigger("Pop");
		_effectOrder++;
	}
}