using UnityEngine;

public class SpecialAttackRocket : MonoBehaviour
{
    [Header("Rocket Settings")]
    public float speed = 10f;       // Speed of the rocket
    public float targetX = 20f;     // Where it stops/destroys
    public int damage = 1000;
    public int health = 1;
    
        // How much damage the rocket deals

    void Update()
    {
        // Move rocket to the right
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Destroy if it passes the target position
      
        if (transform.position.x >= targetX)
        {
            gameObject.SetActive(false);
        }
        
        if (health <= 0)
        {
            DestroySpecialAtt();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Enemy
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            health = health--;
            enemy.TakeDamage(damage);
            return;
        }

        // Asteroid
        Asrtroid asteroid = collision.gameObject.GetComponent<Asrtroid>();
        if (asteroid != null)
        {
            health = health--;
            asteroid.TakeDamage(damage, true);
            return;
        }
    }

    public void DestroySpecialAtt()
    {
      
            gameObject.SetActive (false);
       
    }
}

