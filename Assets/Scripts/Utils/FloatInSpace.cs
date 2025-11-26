using UnityEngine;

public class FloatInSpace : MonoBehaviour
{
    [SerializeField] float speedmModifier = 1;
    private void Update()
    {
        float moveX = GameManger.Instance.adjustedworldSpeed * speedmModifier;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }
    }
}
