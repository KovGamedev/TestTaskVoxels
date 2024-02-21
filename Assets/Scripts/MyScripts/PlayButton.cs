using System.Collections;
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
        Debug.Log(Touchscreen.current.position.ReadValue());
        TryPressToPlayButton(Camera.main.ScreenPointToRay(Touchscreen.current.position.ReadValue()));
    }

    private IEnumerator RunAxeAnimation()
    {
        yield return new WaitForSeconds(_axeAnimatoinDelay);
        _axeAnimator.enabled = true;
    }
}
