using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _weaponRevealingPoint;
    [SerializeField] private Transform _weaponReadinessPoint;
    [SerializeField] private float _weaponSwitchingTime;
    [SerializeField] private Ease _weaponSwitchingEasing;
    [SerializeField] private Wand _wand;

    private LootType _weaponType;

    public void Attack()
    {
        if(_weaponType == LootType.None)
            return;

        switch(_weaponType) {
            case LootType.Wand:
                _wand.Attack();
                break;
            default:
                Debug.LogError($"Weapon type is not implemented: {_weaponType}");
                break;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent<Loot>(out var loot)) {
            switch(loot.Type) {
                case LootType.Wand:
                    _wand.gameObject.SetActive(true);
                    SwitchWeapon(_wand.transform);
                    break;
                default:
                    Debug.LogError($"Loot type is not implemented: {loot.Type}");
                    break;
            }
            _weaponType = loot.Type;
            loot.PickUp();
            Destroy(collider.gameObject);
        }
    }

    private void SwitchWeapon(Transform weapon)
    {
        weapon.localPosition = _weaponRevealingPoint.localPosition;
        weapon.DOLocalMove(_weaponReadinessPoint.localPosition, _weaponSwitchingTime)
            .SetEase(_weaponSwitchingEasing)
            .Play();
    }
}
