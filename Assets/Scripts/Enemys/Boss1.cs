using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private Animator animator;
    private float speedX;
    private float speedY;
    private bool Charging;

    private float switchInterval;
    private float switchTimer;

    private int lives;
    void Start()
    {
        animator = GetComponent<Animator>();
        EnterChargeState();
        lives = 100;
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
            Destroy(gameObject);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(0);
        }
    }
}
