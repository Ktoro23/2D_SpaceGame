using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;

    [SerializeField] private int lives;
    [SerializeField] private int maxLives;
    [SerializeField] private int damage;
    [SerializeField] private int experienceToGive;

    private void OnEnable()
    {
        lives = maxLives;
    }
    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(lives > 0)
        {
            flashWhite.Flash();
        }
        else
        {
            GameObject effect = PoolHelper.GetPool(PoolTypes.BeetlePop).GetPooledObject(transform.position, transform.rotation);
            gameObject.SetActive(false);
            flashWhite.Rest();

        }
    }

}
