using UnityEngine;
using System.Collections;

public class Asrtroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject desroyEffect;
    [SerializeField] private int lives;
    private FlashWhite flashWhite;

    private Rigidbody2D rb;

    [SerializeField] AudioClip[] rockHit;

    [SerializeField] private Sprite[] sprites;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        flashWhite = GetComponent<FlashWhite>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1);

        rb.linearVelocity = new Vector2(pushX, pushY);

        float randomScale = Random.Range(0.4f, 1f);
        transform.localScale = new Vector2(randomScale, randomScale);
       
    }

    
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        
        SoundsFXManager.Instance.PlayRandomSoundFXClip(rockHit, transform, 1f);
        lives -= damage;
        flashWhite.Flash();
        if (lives <= 0)
        {
            Instantiate(desroyEffect, transform.position, transform.rotation);
            SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.boom2, 1f);
            Destroy(gameObject);
        }
    }

   
}
