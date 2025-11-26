using UnityEngine;

// This script controls the movement and wave pattern of the coin.
// It is designed to ignore the player's full boost speed but slightly
// accelerate with the world for an "organic" feel.
public class CoinMovement : MonoBehaviour
{
    // --- Wave Parameters (Set in Inspector) ---
    [Header("Wave Settings")]
    public PathType pathType = PathType.SineVertical;
    public float frequency = 1f;   // How many waves appear (e.g., 1 is a smooth wave)
    public float amplitude = 1.5f; // How wide the wave is

    // --- Organic Movement Parameters ---
    [Header("Organic Motion")]
    [Tooltip("The percentage of the player's boost speed the coin will inherit (e.g., 0.2 means 20%).")]
    [Range(0f, 0.5f)]
    public float boostInheritanceFactor = 0.2f; // Inherit 20% of the boost speed

    private float timer;
    private float centerY;
    private float initialX;
    private float totalTime;

    // --- World Speed Constants ---
    private GameManger gameManager;

    public enum PathType
    {
        Straight,
        SineVertical,
        SineDiagonal
    }

    private void OnEnable()
    {
        // Reset state
        timer = 0;
        totalTime = 0;
        initialX = transform.position.x;
        centerY = transform.position.y;

        // Ensure GameManger reference
        if (gameManager == null)
        {
            gameManager = GameManger.Instance;
        }
    }

    private void Update()
    {
        totalTime += Time.deltaTime;

        // --- 1. CALCULATE MOVEMENT SPEED ---
        float baseSpeed = gameManager.baseScrollSpeed;
        float adjustedSpeed = gameManager.worldSpeed;

        // Calculate the difference between the current world speed and the base speed (the boost effect)
        float boostDifference = adjustedSpeed - baseSpeed;

        // Apply only a small percentage of the boost difference to the coin's movement
        float finalScrollSpeed = baseSpeed + (boostDifference * boostInheritanceFactor);

        // Convert to distance per frame
        float moveX = finalScrollSpeed * Time.deltaTime;


        // --- 2. CALCULATE SINE WAVE (Vertical Component) ---
        float sine = 0f;
        if (pathType == PathType.SineVertical || pathType == PathType.SineDiagonal)
        {
            // Use time for a continuous wave motion
            sine = Mathf.Sin(totalTime * frequency) * amplitude;
        }

        // --- 3. APPLY POSITION ---
        Vector3 newPosition = transform.position;

        // Apply horizontal movement (scrolling)
        newPosition.x -= moveX;

        // Apply vertical wave motion
        newPosition.y = centerY + sine;

        // If diagonal, slightly adjust the Y position based on horizontal progress
        if (pathType == PathType.SineDiagonal)
        {
            newPosition.y += (initialX - newPosition.x) * 0.05f; // Small diagonal drift
        }

        transform.position = newPosition;

        // Check if the coin is off-screen
        if (transform.position.x < -11f)
        {
            gameObject.SetActive(false);
        }
    }
}
