using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    private Animator animator;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float boost = 1f;
    private float boostPower = 5f;

    private Vector2 moveInput;

    InputAction boostAction;

    float horizontalMovement;


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
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);

        Boost();


    }
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Boost()
    {
        if (boostAction.IsPressed())
        {
            boost = boostPower;
            animator.SetBool("boosting", true);
        }
        else
        {
            animator.SetBool("boosting", false);
            boost = 1f;
        }
    }
}
