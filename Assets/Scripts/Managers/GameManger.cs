using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    public float worldSpeed;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
