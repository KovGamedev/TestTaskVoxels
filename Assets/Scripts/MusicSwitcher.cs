using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    [SerializeField] private AudioClip _ambientMusic;
    [SerializeField] private AudioClip _fightMusic;
    [SerializeField] private AudioSource _audioSource;

    public void SwitchToAmbient()
    {
        _audioSource.clip = _ambientMusic;
        _audioSource.Play();
    }

    public void SwitchToFight()
    {
        _audioSource.clip = _fightMusic;
        _audioSource.Play();
    }

    public void StopMusic() => _audioSource.Stop();
}
