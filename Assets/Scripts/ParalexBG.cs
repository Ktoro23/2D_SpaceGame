using UnityEngine;

public class ParalaxBG : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    float backgroundImageWidth;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundImageWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    void Update()
    {
        float moveX = moveSpeed * (PlayerMovement.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(moveX, 0);
        if (Mathf.Abs(transform.position.x) - backgroundImageWidth > 0)
        {
            transform.position = new Vector3(moveX, transform.position.y);
        }
    }
}
