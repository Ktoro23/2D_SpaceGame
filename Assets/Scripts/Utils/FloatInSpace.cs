using UnityEngine;

public class FloatInSpace : MonoBehaviour
{
    private void Update()
    {
        float moveX = GameManger.Instance.adjustedworldSpeed;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }
    }
}
