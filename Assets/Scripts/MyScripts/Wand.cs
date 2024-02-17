using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    [SerializeField] private Transform _model;
    [SerializeField] private GameObject _magicProjectilePrefab;
    [SerializeField] private float _throwingStrength;
    [SerializeField] private Transform _spawnPoint;

    private void Update()
    {
        if(Input.GetMouseButton(0)) {
            var projectile = Instantiate(_magicProjectilePrefab);
            projectile.transform.position = _spawnPoint.position;
            projectile.GetComponent<Rigidbody>().velocity = _throwingStrength * Camera.main.transform.forward;
        }
    }
}
