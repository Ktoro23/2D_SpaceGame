using UnityEngine;

public class Boss1 : Enemy
{
    private Animator animator;
 
    private bool Charging;

    private float switchInterval;
    private float switchTimer;

    //private ObjectPooler destroyEffectPool;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        EnterChargeState();
        
    }
   
    public override void Start()
    {
        base.Start();
        destroyEffectPool = PoolHelper.GetPool(PoolTypes.Boss1Boom);
        hitSound = SoundsFXManager.Instance.bossHit;
        destroySound = SoundsFXManager.Instance.boom2;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();  
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
        speedY = Random.Range(-1f, 1f);
        switchInterval = Random.Range(5f, 10f);
        switchTimer = switchInterval;
        Charging = false;
        animator.SetBool("Charging", false);
    }

    void EnterChargeState()
    {
        animator.SetBool("Charging", true);
        if (!Charging) SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.bossCharge, 1f, true);
        speedX = -5f;
        speedY = 0;
        switchInterval = Random.Range(0.6f, 1.5f);
        switchTimer = switchInterval;
        Charging = true;        
    }


    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asrtroid asrtroid = collision.gameObject.GetComponent<Asrtroid>();
            if (asrtroid) asrtroid.TakeDamage(damage, false);           
        }
        
    }

    protected override void PlayDeathAnim()
    {

        GameObject effect = PoolHelper.GetPool(PoolTypes.Boss1Boom).GetPooledObject(transform.position, transform.rotation);

    }
}
