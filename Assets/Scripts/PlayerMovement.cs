using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    private Animator animator;
    public Rigidbody2D rb;
    private Vector2 playerDirection;
    private SpriteRenderer spriteRenderer;

    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;

    public float moveSpeed = 5f;
    public float boost = 1f;
    private float boostPower = 4f;
    private bool boosting = false;

    private Vector2 moveInput;

    InputAction boostAction;

    private PlayerInput playerInput;
    private InputAction shootAction;

    float horizontalMovement;

    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private GameObject destroyEffect;

    [SerializeField] private ParticleSystem engineEffect;

    //[SerializeField] AudioClip OnDeathSound;



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

        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
    }
        
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        defaultMaterial = spriteRenderer.material;

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
            moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
            animator.SetFloat("moveX", moveInput.x);
            animator.SetFloat("moveY", moveInput.y);
            Boost();

        }
     

        Debug.Log("moveInput: " + moveInput);
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
   // public void Move(InputAction.CallbackContext context)
   // {
   //     moveInput = context.ReadValue<Vector2>();
  //  }

    public void Boost()
    {
        
        if (boostAction.IsPressed())
        {
           
            if (energy > 10f)
            {
                
                boost = boostPower;
                animator.SetBool("boosting", true);
                boosting = true;
                engineEffect.Play();
               // SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.Boosting, 1f);
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
        engineEffect.Stop();
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
        SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.Hit, 1f);
        spriteRenderer.material = whiteMaterial;
        StartCoroutine("ResetMaterial");
        if (health <= 0)
        {
            boost = 0f;
            SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.OnDeathSound, 1f);
            gameObject.SetActive(false);
            Instantiate(destroyEffect,transform.position, transform.rotation);
            GameManger.Instance.GameOver();
            
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }

    void OnShoot (InputAction.CallbackContext context)
    {       
        PhaserWeapon.Instance.Shoot();
    }
    void OnEnable()
    {
        shootAction.performed += OnShoot;
    }

    void OnDisable()
    {
        shootAction.performed -= OnShoot;
    }
}
