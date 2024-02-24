using UnityEngine;

public class StoneDestructionSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void Paly() => _audioSource.Play();
}
