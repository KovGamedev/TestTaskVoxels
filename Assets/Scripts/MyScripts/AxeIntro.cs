using UnityEngine;
using UnityEngine.Events;

public class AxeIntro : MonoBehaviour
{
    [SerializeField] private UnityEvent _animationEndEvent = new();

    public void OnAnimationEnd() => _animationEndEvent.Invoke();
}
