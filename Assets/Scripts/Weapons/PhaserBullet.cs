using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    [SerializeField] string[] collisionTags;
    private void Update()
    {
        transform.position += new Vector3(PhaserWeapon.Instance.speed * Time.deltaTime, 0f);
        if (transform.position.x > 11)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Critter") || )
        foreach (string tag in collisionTags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                gameObject.SetActive(false);
                return;
            }
        }
    }
}


