using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VoxelDestruction;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private float _destructionStrength;
    [SerializeField] private VoxelObject _voxelObject;
    [SerializeField] private LevelChanger _levelChanger;
    [Header("Axe animation")]
    [SerializeField] private float _axeAnimatoinDelay;
    [SerializeField] private Animator _axeAnimator;

    private InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Intro.Enable();

        _inputActions.Intro.Click.performed += OnClick;
        _inputActions.Intro.Touch.performed += OnTouch;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        TryPressToPlayButton(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));
    }

    private void TryPressToPlayButton(Ray inputRay)
    {
        if(Physics.Raycast(inputRay, out var hitInfo)) {
            if(hitInfo.transform.GetComponentInParent<PlayButton>() != null) {
                var point = transform.position;
                var normal = Vector3.zero;
                _voxelObject.AddDestruction(_destructionStrength, point, normal);
                StartCoroutine(RunAxeAnimation());
            }
        }
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        TryPressToPlayButton(Camera.main.ScreenPointToRay(Touchscreen.current.position.ReadValue()));
    }

    private IEnumerator RunAxeAnimation()
    {
        yield return new WaitForSeconds(_axeAnimatoinDelay);
        _axeAnimator.enabled = true;
    }

    private void OnDestroy()
    {
        _inputActions.Intro.Click.performed -= OnClick;
        _inputActions.Intro.Touch.performed -= OnTouch;
    }
}
