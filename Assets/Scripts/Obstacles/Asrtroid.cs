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
    private int maxLive = 5;
    private int damage = 1;
    private int expirenceToGive = 1;
    float pushX;
    float pushY;

 

    private void OnEnable()
    {
        lives = maxLive;
        transform.rotation = Quaternion.identity;
        pushX = Random.Range(-1f, 0);
        pushY = Random.Range(-1f, 1);
        if (rb) rb.linearVelocity = new Vector2(pushX, pushY);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        flashWhite = GetComponent<FlashWhite>();
        destroyEffectPool = PoolHelper.GetPool(PoolTypes.Astroid);
       

        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        float randomScale = Random.Range(0.4f, 1f);
        transform.localScale = new Vector2(randomScale, randomScale);

        lives = maxLive;

    }

    
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player) player.TakeDamage(damage);
        }
       
    }

    public void TakeDamage(int damage, bool giveExperience)
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
            if (giveExperience)
            {
                PlayerMovement.Instance.GetExperience(expirenceToGive);
            }
        }
    }

   
}
