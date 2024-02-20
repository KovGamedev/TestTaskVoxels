using UnityEngine;

public class Wand : MonoBehaviour, IAttacking
{
    [SerializeField] private Transform _model;
    [SerializeField] private GameObject _magicProjectilePrefab;
    [SerializeField] private float _throwingStrength;
    [SerializeField] private Transform _spawnPoint;

    public void Attack()
    {
        var projectile = Instantiate(_magicProjectilePrefab);
        projectile.transform.position = _spawnPoint.position;
        projectile.GetComponent<Rigidbody>().velocity = _throwingStrength * Camera.main.transform.forward;
    }
}
