using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _lookingSpeed;

    private bool _isMouseButtonHeld = false;

    private InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Enable();

        _inputActions.Player.MouseHold.started += SetMouseButtonPressed;
        _inputActions.Player.MouseHold.canceled += SetMouseButtonUnpressed;
    }

    private void SetMouseButtonPressed(InputAction.CallbackContext context) => _isMouseButtonHeld = true;

    private void SetMouseButtonUnpressed(InputAction.CallbackContext context) => _isMouseButtonHeld = false;

    private void FixedUpdate()
    {
        Move(_inputActions.Player.Movement.ReadValue<Vector2>());
        if(_isMouseButtonHeld)
            Look(_inputActions.Player.Look.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(_movementSpeed * new Vector3(direction.x, 0, direction.y));
    }

    public void Look(Vector2 direction)
    {
        var invertedDirection = -direction;
        var bodyRotationSummand = new Vector3(0, _lookingSpeed * invertedDirection.x, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + bodyRotationSummand);
        var targetRotationX = Camera.main.transform.localRotation.eulerAngles.x - _lookingSpeed * invertedDirection.y;
        Camera.main.transform.localRotation = Quaternion.Euler(targetRotationX, 0, 0);
    }

    private void OnDestroy()
    {
        _inputActions.Player.MouseHold.started -= SetMouseButtonPressed;
        _inputActions.Player.MouseHold.canceled -= SetMouseButtonUnpressed;
    }
}
