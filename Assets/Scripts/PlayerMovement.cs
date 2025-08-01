using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    private Animator animator;
    public Rigidbody2D rb;
    private Vector2 playerDirection;
    private FlashWhite flashWhite;


    public float moveSpeed = 5f;

    public bool boosting = false;

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

    private ObjectPooler destroyEffectPool;

    [SerializeField] private ParticleSystem engineEffect;

    [SerializeField] private int experience;
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxLevel;

    [SerializeField] private List<int> playerLevels;



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
        shootAction = InputSystem.actions.FindAction("Shoot");
        boostAction = InputSystem.actions.FindAction("Sprint");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flashWhite = GetComponent<FlashWhite>();

        for (int i = playerLevels.Count; i <maxLevel; i++)
        {
            playerLevels.Add(Mathf.CeilToInt (playerLevels[playerLevels.Count - 1] * 1.1f + 15));
        }

        destroyEffectPool = PoolHelper.GetPool(PoolTypes.Boom1);
        energy = maxEnergy;
        UIController.Instance.updateEnergySlider(energy, maxEnergy);
        health = maxHealth;
        UIController.Instance.updateHealthSlider(health, maxHealth);
        experience = 0;
        UIController.Instance.updateExperienceSlider(experience, playerLevels[currentLevel]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
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
            if (energy >= 0.5f) energy -= 0.5f;
            else
            {
                ExitBoost();
            }
        }
        else
        {
            if (energy < maxEnergy)
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

                GameManger.Instance.SetWorldSpeed(7f);
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
        GameManger.Instance.SetWorldSpeed(1f);
        boosting = false;
        engineEffect.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            Asrtroid asrtroid = collision.gameObject.GetComponent<Asrtroid>();
            if (asrtroid) asrtroid.TakeDamage(1, false);

        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UIController.Instance.updateHealthSlider(health, maxHealth);
        SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.Hit, 1f);
        flashWhite.Flash();
        if (health <= 0)
        {
            ExitBoost();
            GameManger.Instance.SetWorldSpeed(0f);
            SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.OnDeathSound, 1f);
            GameObject effect =  PoolHelper.GetPool(PoolTypes.Boom1).GetPooledObject(transform.position );

            GameManger.Instance.GameOver();
            gameObject.SetActive(false);
        }
    }

    void OnShoot(InputAction.CallbackContext context)
    {
        PhaserWeapon.Instance.Shoot();
    }
    void OnEnable()
    {
        shootAction.performed += OnShoot;
        boostAction.performed += OnBoost;
        boostAction.Enable();
    }

    void OnDisable()
    {
        shootAction.performed -= OnShoot;
        boostAction.performed -= OnBoost;
    }

    void OnBoost(InputAction.CallbackContext context)
    {
        if (energy > 10f)
        {
            engineEffect.Play();
            SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.Boosting, 1f);
        }
    }

    public void GetExperience(int exp)
    {
        experience += exp;
        UIController.Instance.updateExperienceSlider(experience, playerLevels[currentLevel]);
        if (experience >= playerLevels[currentLevel])
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        experience -= playerLevels[currentLevel];
        if (currentLevel < maxLevel - 1) currentLevel++;
        UIController.Instance.updateExperienceSlider(experience, playerLevels[currentLevel]);
        PhaserWeapon.Instance.LevelUp();
        maxHealth++;
        health = maxHealth;
        UIController.Instance.updateHealthSlider(health, maxHealth);
    }
}
