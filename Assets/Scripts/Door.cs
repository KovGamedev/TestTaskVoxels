using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelDestruction;

public class Door : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _destructionStrength;
    [SerializeField] private VoxelObject _voxelObject;

    private void OnTriggerEnter(Collider collider) => TryDestroyByAxe(collider);

    private void OnTriggerStay(Collider collider) => TryDestroyByAxe(collider);

    private void TryDestroyByAxe(Collider collider)
    {
        if(collider.transform.TryGetComponent<Axe>(out var axe)) {
            _audioSource.Play();
            var point = collider.ClosestPoint(axe.transform.position);
            var normal = Vector3.forward;
            _voxelObject.AddDestruction(_destructionStrength, point, normal);
        }
    }
}
