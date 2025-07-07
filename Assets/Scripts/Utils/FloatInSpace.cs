using UnityEngine;

public class FloatInSpace : MonoBehaviour
{
    private void Update()
    {
        float moveX = GameManger.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
    }
}
