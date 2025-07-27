using UnityEngine;
using System.Collections;

public class Asrtroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ObjectPooler destroyEffectPool;
    
    private FlashWhite flashWhite;

    private Rigidbody2D rb;

    [SerializeField] AudioClip[] rockHit;

    [SerializeField] private Sprite[] sprites;

    private int lives;
    private int maxLive;
    private int damage;
    private int expirenceToGive = 1;

    private void OnEnable()
    {
        lives = maxLive;
        transform.rotation = Quaternion.identity;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        flashWhite = GetComponent<FlashWhite>();
        destroyEffectPool = PoolHelper.GetPool(PoolTypes.Astroid);
        
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1);

        rb.linearVelocity = new Vector2(pushX, pushY);

        float randomScale = Random.Range(0.4f, 1f);
        transform.localScale = new Vector2(randomScale, randomScale);

        maxLive = 5;
        lives = maxLive;
        damage = 1;
    }

    
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player) player.TakeDamage(damage);
        }
       
    }

    public void TakeDamage(int damage)
    {
        
        SoundsFXManager.Instance.PlayRandomSoundFXClip(rockHit, transform, 1f);
        lives -= damage;
        if (lives > 0)
        {
            flashWhite.Flash();
        }
        
        else
        {
            GameObject effect = PoolHelper.GetPool(PoolTypes.Boom2).GetPooledObject(transform.position);
            effect.transform.localScale = transform.localScale;
            SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.boom2, 1f);
            flashWhite.Rest();
            gameObject.SetActive(false);
            PlayerMovement.Instance.GetExperience(expirenceToGive);
        }
    }

   
}
