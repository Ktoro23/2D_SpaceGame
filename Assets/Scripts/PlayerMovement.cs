using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    private Animator animator;
    public Rigidbody2D rb;
    private Vector2 playerDirection;

    public float moveSpeed = 5f;
    public float boost = 1f;
    private float boostPower = 5f;
    private bool boosting = false;

    private Vector2 moveInput;

    InputAction boostAction;

    float horizontalMovement;

    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private GameObject destroyEffect;

    [SerializeField] AudioClip OnDeathSound;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
        
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        boostAction = InputSystem.actions.FindAction("Sprint");
        energy = maxEnergy;
        UIController.Instance.updateEnergySlider(energy, maxEnergy);
        health = maxHealth;
        UIController.Instance.updateHealthSlider(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
       if (Time.timeScale > 0 )
       {
            animator.SetFloat("moveX", moveInput.x);
            animator.SetFloat("moveY", moveInput.y);
            Boost();

       }    


    }

    private void FixedUpdate()
    {
        // rb.linearVelocity = new Vector2(playerDirection.x * moveSpeed, playerDirection.y * moveSpeed);
        rb.linearVelocity = moveInput * moveSpeed;
        if (boosting)
        {
           if (energy >= 0.2f) energy -= 0.2f;
            else
            {
                ExitBoost();
            }
        }
        else
        {
            if(energy < maxEnergy)
            {
                energy += energyRegen;
            }
        }
        UIController.Instance.updateEnergySlider(energy, maxEnergy);
    }
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Boost()
    {
        
        if (boostAction.IsPressed())
        {
            if(energy > 10f)
            {
                boost = boostPower;
                animator.SetBool("boosting", true);
                boosting = true;
            }
            
        }
        else
        {
            ExitBoost();
        }
       
    }

    public void ExitBoost()
    {
        animator.SetBool("boosting", false);
        boost = 1f;
        boosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        UIController.Instance.updateHealthSlider(health, maxHealth);
        if (health <= 0)
        {
            boost = 0f;
            SoundsFXManager.Instance.PlaySoundFXClip(OnDeathSound, 50);
            gameObject.SetActive(false);
            Instantiate(destroyEffect,transform.position, transform.rotation);
            GameManger.Instance.GameOver();
            
        }
    }
}
