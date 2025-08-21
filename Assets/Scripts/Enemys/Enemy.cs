using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    protected SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;
    protected ObjectPooler destroyEffectPool;

    [SerializeField] protected int lives;
    [SerializeField] protected int maxLives;
    [SerializeField] protected int damage;
    [SerializeField] protected int experienceToGive;

    protected AudioClip hitSound;
    protected AudioClip destroySound;

    protected float speedX = 0;
    protected float speedY = 0;


    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void OnEnable()
    {
        lives = maxLives;
    }
    public virtual void Start()
    {
        
        flashWhite = GetComponent<FlashWhite>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player) player.TakeDamage(damage);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        lives -= damage;
        SoundsFXManager.Instance.PlaySoundFXClip(hitSound, 1f, true);
        if (lives > 0)
        {
            flashWhite.Flash();
        }
        else
        {
            SoundsFXManager.Instance.PlaySoundFXClip(destroySound, 1f);
            PlayDeathAnim();
            PlayerMovement.Instance.GetExperience(experienceToGive);
            gameObject.SetActive(false);
            flashWhite.Rest();

        }
    }

    protected virtual void PlayDeathAnim()
    {
        GameObject effect = PoolHelper.GetPool(PoolTypes.BeetlePop).GetPooledObject(transform.position, transform.rotation);
    }
}
