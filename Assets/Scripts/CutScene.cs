using UnityEngine;
using UnityEngine.Playables;

public class CutScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    public void StartScene() => _playableDirector.enabled = true;
}
