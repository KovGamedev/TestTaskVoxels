using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _weaponRevealingPoint;
    [SerializeField] private Transform _weaponReadinessPoint;
    [SerializeField] private float _weaponSwitchingTime;
    [SerializeField] private Ease _weaponSwitchingEasing;
    [SerializeField] private Transform _wand;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent<Loot>(out var loot)) {
            switch(loot.Type) {
                case LootType.Wand:
                    _wand.gameObject.SetActive(true);
                    SwitchWeapon(_wand);
                    break;
                default:
                    Debug.LogError($"Loot type is not implemented: {loot.Type}");
                    break;
            }

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
