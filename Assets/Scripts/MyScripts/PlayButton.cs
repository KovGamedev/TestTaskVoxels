using UnityEngine;
using UnityEngine.InputSystem;
using VoxelDestruction;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private float _destructionStrength;
    [SerializeField] private VoxelObject _voxelObject;

    public void OnClick(InputAction.CallbackContext context)
    {
        var inputRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(inputRay, out var hitInfo)) {
            Debug.Log(hitInfo.transform.name);
            if(hitInfo.transform.GetComponentInParent<PlayButton>() != null) {
                var point = transform.position;
                var normal = Vector3.zero;
                _voxelObject.AddDestruction(_destructionStrength, point, normal);
            }
        }
    }
}