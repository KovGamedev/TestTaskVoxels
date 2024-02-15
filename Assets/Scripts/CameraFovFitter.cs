using Sirenix.OdinInspector;
using UnityEngine;

public class CameraFovFitter : MonoBehaviour
{
    [SerializeField] private Vector2 targetResolution;
    [SerializeField] private float targetFov;

    private Camera _camera;
    private void Start()
    {
        _camera = GetComponent<Camera>();
        UpdateFov();
    }

    [Button]
    private void UpdateFov()
    {
        float targetAspect = targetResolution.x / targetResolution.y;
        float currentAspect = _camera.aspect;
        float fovFactor = targetAspect / currentAspect;
        _camera.fieldOfView = targetFov * fovFactor;
    }
}