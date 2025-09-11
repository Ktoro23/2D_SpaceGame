using UnityEngine;
using UnityEngine.SceneManagement;

public class LostWhale : MonoBehaviour
{
    [SerializeField] GameObject UI;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManger.Instance.ShowLevelComplete();
        }
    }
}
