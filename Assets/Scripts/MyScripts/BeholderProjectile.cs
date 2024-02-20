using UnityEngine;

public class BeholderProjectile : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform; // Stupid Oldi inspector can not save link to scene object
        transform.LookAt(_player.position);
    }

    private void FixedUpdate() => transform.position += _speed * transform.forward;

    private void OnCollisionEnter(Collision collision)
    {
        var magicProjectile = collision.transform.GetComponentInParent<MagicProjectile>();
        if(magicProjectile != null)
            Destroy(gameObject);
    }
}
