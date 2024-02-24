using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelDestruction;

public class FloorWithTraps : MonoBehaviour
{
    [SerializeField] private float _destructionStrength;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.TryGetComponent<Player>(out var player)) {
            var point = collider.ClosestPoint(player.transform.position);
            var normal = Vector3.down;
            transform.GetComponentInParent<VoxelObject>()?.AddDestruction(_destructionStrength, point, normal);
        }
    }
}
