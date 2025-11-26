using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // This is the main method for activating the power-up.
    // Your specific scripts (like Shield.cs) will OVERRIDE this.
    protected virtual void Activate(GameObject player)
    {
        // Base implementation does nothing.
        // Your child script (e.g., Shield.cs) will replace this logic.
        Debug.Log("Base PowerUp.Activate() called. Did you forget to override it?");
    }

    // This handles the collision logic for ALL power-ups.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Call the specific Activate method (e.g., Shield.cs's Activate)
            Activate(other.gameObject);

            // 2. Play a pickup sound (optional)
            // ...

            // 3. Destroy the power-up object
            Destroy(gameObject);
        }
    }

    // Optional: Make the power-up move down the screen
    private void Start()
    {
        // Example: Make it move down slowly
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true; // So it's not affected by physics
            rb.linearVelocity = Vector2.down * 1.5f; // Adjust speed as needed
        }

        // Make sure the trigger is set
        GetComponent<Collider2D>().isTrigger = true;
    }
}
