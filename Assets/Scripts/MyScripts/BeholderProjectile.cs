using UnityEngine;

public class BeholderProjectile : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;

    private void Start() => transform.LookAt(_player.position);

    private void FixedUpdate() => transform.position += _speed * transform.forward;

    private void OnCollisionEnter(Collision collision)
    {
        var magicProjectile = collision.transform.GetComponentInParent<MagicProjectile>();
        if(magicProjectile != null)
            Destroy(gameObject);
    }
}
