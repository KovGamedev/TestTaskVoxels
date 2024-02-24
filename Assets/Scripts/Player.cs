using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _weaponRevealingPoint;
    [SerializeField] private Transform _weaponReadinessPoint;
    [SerializeField] private float _weaponSwitchingTime;
    [SerializeField] private Ease _weaponSwitchingEasing;
    [SerializeField] private Wand _wand;
    [SerializeField] private Axe _axe;
    [SerializeField] private float _controlEnablingDelay;
    [SerializeField] private PlayerInteractions _playerInteractions;

    private WeaponType _weaponType;

    public void Attack()
    {
        if(_weaponType == WeaponType.None)
            return;

        switch(_weaponType) {
            case WeaponType.Wand:
                _wand.Attack();
                break;
            case WeaponType.Axe:
                _axe.Attack();
                break;
            default:
                Debug.LogError($"Weapon type is not implemented: {_weaponType}");
                break;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent<IWeaponable>(out var weapon)) {
            switch(weapon.GetWeaponType()) {
                case WeaponType.None:
                    DeactivateWeapon(_axe.transform);
                    break;
                case WeaponType.Axe:
                    _axe.gameObject.SetActive(true);
                    ActivateWeapon(_axe.transform);
                    break;
                case WeaponType.Wand:
                    _wand.gameObject.SetActive(true);
                    ActivateWeapon(_wand.transform);
                    break;
                default:
                    Debug.LogError($"Loot type is not implemented: {weapon.GetWeaponType()}");
                    break;
            }
            _weaponType = weapon.GetWeaponType();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.TryGetComponent<IWeaponable>(out var weapon)) {
            DeactivateWeapon(_axe.transform);
            _weaponType = WeaponType.None;
        }
    }

    private void DeactivateWeapon(Transform weapon)
    {
        weapon.DOLocalMove(_weaponRevealingPoint.localPosition, _weaponSwitchingTime)
            .SetEase(_weaponSwitchingEasing)
            .OnComplete(() => weapon.gameObject.SetActive(false))
            .Play();
    }

    private void ActivateWeapon(Transform weapon)
    {
        weapon.localPosition = _weaponRevealingPoint.localPosition;
        weapon.DOLocalMove(_weaponReadinessPoint.localPosition, _weaponSwitchingTime)
            .SetEase(_weaponSwitchingEasing)
            .Play();
    }

    private void Start()
    {
        StartCoroutine(EnableControl());
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator EnableControl() {
        yield return new WaitForSeconds(_controlEnablingDelay);
        _playerInteractions.enabled = true;
        transform.rotation = Quaternion.identity;
    }
}
