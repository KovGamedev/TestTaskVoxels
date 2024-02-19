using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public LootType Type;

    [Header("Rotation")]
    [SerializeField] private Transform _rotatingContainer;
    [SerializeField] private float _rotatingSpeed;
    [Header("LightRay")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Color _rayColor;
    [SerializeField] private float _baseRayTransparency;
    [SerializeField] private float _distanceToHideRay;
    [SerializeField] private float inaccuracy;
    [SerializeField] private Transform _player;

    private void FixedUpdate()
    {
        RotateLoot();
        DrawLightRay();
    }

    private void RotateLoot() => _rotatingContainer.Rotate(0, _rotatingSpeed, 0);

    private void DrawLightRay()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        var endAlpha = Mathf.Clamp(distanceToPlayer / _distanceToHideRay - inaccuracy, 0, _baseRayTransparency);
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(_rayColor, 0f), new GradientColorKey(_rayColor, 1f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0f, 0f), new GradientAlphaKey(endAlpha, 1f) }
        );
        _lineRenderer.colorGradient = gradient;
    }
}
