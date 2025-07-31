using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private Animator animator;
    private ObjectPooler destroyEffectPool;


    private float speedX;
    private float speedY;
    private bool Charging;

    private float switchInterval;
    private float switchTimer;

    private int lives;
    private int maxLives = 100;
    private int damage = 20;
    private int experienceToGive = 20;


    private void OnEnable()
    {
        EnterChargeState();
        lives = maxLives;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }
    void Start()
    {
        destroyEffectPool = PoolHelper.GetPool(PoolTypes.Boss1Boom);
    }

    // Update is called once per frame
    void Update()
    {
        float playerPosition = PlayerMovement.Instance.transform.position.x;
        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            if (Charging && transform.position.x > playerPosition)
            {
                EnterPatrolState();
            }
            else
            {
                EnterChargeState();
            }
        }

        if (transform.position.y > 3 || transform.position.y < -3)
        {
            speedY *= -1;
        }
        else if (transform.position.x < playerPosition)
        {
            EnterChargeState();
        }

        bool boost = PlayerMovement.Instance.boosting;
        float moveX;
        if (boost && !Charging)
        {
            moveX = GameManger.Instance.worldSpeed * Time.deltaTime * -0.5f;
        }
        else
        {
            moveX = speedX * Time.deltaTime;
        }


            float moveY = speedY * Time.deltaTime;
        
        transform.position += new Vector3(moveX, moveY);
        if (transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }
    }

    void EnterPatrolState()
    {
        speedX = 0f;
        speedY = Random.Range(-2f, 2f);
        switchInterval = Random.Range(5f, 10f);
        switchTimer = switchInterval;
        Charging = false;
        animator.SetBool("Charging", false);
    }

    void EnterChargeState()
    {
        animator.SetBool("Charging", true);
        if (!Charging) SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.bossCharge, 1f, true);
        speedX = -10f;
        speedY = 0;
        switchInterval = Random.Range(0.6f, 1.5f);
        switchTimer = switchInterval;
        Charging = true;        
    }

    public void TakeDamage(int damage)
    {
        SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.bossHit, 1f, true);
        lives -= damage;
        if (lives < 0)
        {
            GameObject effect = PoolHelper.GetPool(PoolTypes.Boss1Boom).GetPooledObject(transform.position);
            SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.boom2, 1f, true);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            Asrtroid asrtroid = collision.gameObject.GetComponent<Asrtroid>();
            if (asrtroid) asrtroid.TakeDamage(damage);
            
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player) player.TakeDamage(damage);
        }
    }
}
