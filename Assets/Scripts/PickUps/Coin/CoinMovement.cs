using UnityEngine;

// Define the different movement patterns for the coins.
public enum CoinPathType
{
    Straight,     // Moves only horizontally (left)
    SineVertical, // Moves horizontally and waves up/down (Y-axis)
    SineDiagonal  // Moves horizontally and waves diagonally (X/Y-axis)
}

// This script manages the entire motion (scrolling and waving) of the coin, ignoring player boost.
public class CoinMovement : MonoBehaviour
{
    // --- Inspector Variables ---

    [Header("Movement Settings")]
    [Tooltip("The multiplier for the base world speed. Keep at 1f unless you want coins slower/faster than the world.")]
    [SerializeField] private float speedMultiplier = 1f;

    [Tooltip("The chosen path type for this coin.")]
    [SerializeField] private CoinPathType pathType = CoinPathType.SineVertical;

    [Header("Sine Wave Settings")]
    [Tooltip("How quickly the coin moves through the sine wave (frequency).")]
    [SerializeField] private float frequency = 2f;

    [Tooltip("How high/wide the wave pattern is (amplitude).")]
    [SerializeField] private float amplitude = 1f;

    // --- Private Variables ---
    private float timer;
    private float initialY;
    private float initialX;

    // Variable to hold the CONSTANT world scroll speed.
    private float baseWorldSpeed;

    private void OnEnable()
    {
        // Reset the timer and capture the initial position when the coin spawns.
        timer = 0f;

        // Ensure the coin's base position is captured when it's enabled/spawned.
        initialY = transform.position.y;
        initialX = transform.position.x;

        // Safety check to get the base world speed from the GameManager.
        if (GameManger.Instance != null)
        {
            // CRITICAL: Get the constant speed that NEVER changes with boost.
            baseWorldSpeed = GameManger.Instance.baseScrollSpeed;
        }
        else
        {
            Debug.LogError("GameManger is not available. Coin movement may be incorrect.");
            baseWorldSpeed = 5f; // Fallback speed
        }
    }

    private void Update()
    {
        // Increment the timer for the sine function.
        timer += Time.deltaTime;

        // Calculate the CONSTANT horizontal movement using the base speed.
        // This is the combined scrolling and speed multiplier.
        // The coin is now scrolling left at a constant speed.
        float scrollDeltaX = -1f * baseWorldSpeed * speedMultiplier * Time.deltaTime;

        float newX = transform.position.x + scrollDeltaX;
        float newY = transform.position.y;

        // Calculate the wave position
        float sine = Mathf.Sin(timer * frequency) * amplitude;

        // Apply wave based on the selected path type.
        switch (pathType)
        {
            case CoinPathType.Straight:
                // Horizontal scroll is applied via newX calculation above.
                // Y position remains constant.
                break;

            case CoinPathType.SineVertical:
                // Wave is applied relative to the initial Y position.
                newY = initialY + sine;
                break;

            case CoinPathType.SineDiagonal:
                // Applies the sine to the X position for a diagonal effect
                // We add the sine wave's oscillation to the newX calculation.
                newX = transform.position.x + scrollDeltaX + sine * Time.deltaTime;

                // Applies a smaller, timed sine wave to the Y position
                newY = initialY + Mathf.Sin(timer * frequency + 1f) * (amplitude * 0.5f);
                break;
        }

        // Apply the final calculated position.
        transform.position = new Vector3(newX, newY, transform.position.z);

        // Simple check to recycle the coin if it goes off-screen to the left.
        if (transform.position.x < -10f)
        {
            // Assuming you are using PoolHelper based on your other scripts.
            // If not, replace this line with Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}