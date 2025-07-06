using UnityEngine;

public class Whale : MonoBehaviour
{
    void Update()
    {
        float moveX = GameManger.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -11)
        {
            Destroy(gameObject);
        }
    }
}
