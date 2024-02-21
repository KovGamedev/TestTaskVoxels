using UnityEngine;

public class PLayerCatcher : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out var player)) {
            GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>().Change();
            gameObject.SetActive(false);
        }
    }
}
