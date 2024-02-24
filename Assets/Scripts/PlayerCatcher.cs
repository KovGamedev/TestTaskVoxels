using UnityEngine;

public class PLayerCatcher : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out var player)) {
            _canvas.SetActive(false);
            GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>().Change();
            gameObject.SetActive(false);
        }
    }
}
