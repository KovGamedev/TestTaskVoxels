using UnityEngine;

public class AxeArea : MonoBehaviour, IWeaponable
{
    public WeaponType GetWeaponType() => WeaponType.Axe;
}
