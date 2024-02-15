using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    public event Action OnHit;

    [SerializeField] private float rotationSpeed = 1;

    private Rigidbody rb;

    private NewSingleCharacter _target;
    private float _speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.onUnitSphere * rotationSpeed;
    }

    public void Initialize(NewSingleCharacter target, float speed)
    {
        _target = target;
        _speed = speed;
    }

    private void FixedUpdate()
    {
        if (_target == null || _target.IsDead)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 offset = new Vector3(0, 0.2f, 0);
        Vector3 targetPosition = _target.transform.position + offset;
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, _speed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != _target.transform)
            return;

        OnHit?.Invoke();
        Destroy(gameObject);
    }
}