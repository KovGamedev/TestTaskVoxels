using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using VoxelDestruction;

public class MagicProjectile : MonoBehaviour
{
    [SerializeField, Min(0)] private float _strength;
    [SerializeField, Min(0)] private float _selfDestructionTimer;

    private void Start() => StartCoroutine(DestroySelfCoroutine());

    private IEnumerator DestroySelfCoroutine() {
        yield return new WaitForSeconds(_selfDestructionTimer);
        DestroySelf();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var point = collision.contacts[0].point;
        var normal = (collision.contacts[0].point - transform.position).normalized;
        collision.transform.GetComponentInParent<VoxelObject>()?.AddDestruction(_strength, point, normal);
        DestroySelf();
    }

    private void DestroySelf() => Destroy(gameObject);
}
