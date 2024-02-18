using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _lookingSpeed;
    [SerializeField] private Wand _wand;

    private InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Enable();

        _inputActions.Player.Attack.performed += Attack;
    }

    private void Attack(InputAction.CallbackContext context) {
        _wand.Attack();
    }

    private void FixedUpdate()
    {
        Move(_inputActions.Player.Movement.ReadValue<Vector2>());
        Look(_inputActions.Player.Look.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        transform.position += _movementSpeed * new Vector3(direction.x, 0, direction.y);
    }

    public void Look(Vector2 direction)
    {
        var rotationSummand = new Vector3(-_lookingSpeed * direction.y, _lookingSpeed * direction.x, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationSummand);
    }
}
