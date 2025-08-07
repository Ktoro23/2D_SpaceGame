using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;

    [SerializeField] private int lives;
    [SerializeField] private int maxLives;
    [SerializeField] private int damage;
    [SerializeField] private int experienceToGive;

    protected AudioClip hitSound;
    protected AudioClip destroySound;

    protected float speedX = 0;
    protected float speedY = 0;

    public virtual void OnEnable()
    {
        lives = maxLives;
    }
    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime);
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
        lives -= damage;
        SoundsFXManager.Instance.PlaySoundFXClip(hitSound, 1f, true);
        if (lives > 0)
        {
            flashWhite.Flash();
        }
        else
        {
            SoundsFXManager.Instance.PlaySoundFXClip(destroySound, 1f);
            GameObject effect = PoolHelper.GetPool(PoolTypes.BeetlePop).GetPooledObject(transform.position, transform.rotation);
            PlayerMovement.Instance.GetExperience(experienceToGive);
            gameObject.SetActive(false);
            flashWhite.Rest();

        }
    }

}
