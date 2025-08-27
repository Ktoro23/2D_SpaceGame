using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.ActivateShield();
            gameObject.SetActive(false); // or Destroy(gameObject) if not pooled
        }
    }
}
