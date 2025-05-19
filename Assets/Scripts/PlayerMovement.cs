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
    }

    // Update is called once per frame
    void Update()
    {
        
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);

        Boost();
       
     


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
}
