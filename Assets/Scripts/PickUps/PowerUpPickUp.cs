using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PowerUpManager.Instance != null)
            {
                PowerUpManager.Instance.ActivatePowerUp();
            }
            gameObject.SetActive(false); // or Destroy(gameObject);
        }
    }
}
