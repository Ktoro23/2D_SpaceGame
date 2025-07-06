using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private Animator animator;
    private float speedX;
    private float speedY;
    private bool Charging;

    private float switchInterval;
    private float switchTimer;
    void Start()
    {
        animator = GetComponent<Animator>();
        EnterChargeState();
    }

    // Update is called once per frame
    void Update()
    {
        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            if (Charging)
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
        float moveX = speedX * PlayerMovement.Instance.boost * Time.deltaTime;
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
        speedX = -5f;
        speedY = 0;
        switchInterval = Random.Range(2f, 2.5f);
        switchTimer = switchInterval;
        Charging = true;
        animator.SetBool("Charging", true);
        SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.bossCharge, 1, true);

        
    }

    public void TakeDamage(int damage)
    {

    }
}
