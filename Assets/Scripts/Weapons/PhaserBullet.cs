using UnityEngine;

public class PhaserBullet : MonoBehaviour
{

    PhaserWeapon weapon;

    private void Start()
    {
        weapon = PhaserWeapon.Instance;
    }
    private void Update()
    {
        transform.position += new Vector3(weapon.stats[weapon.weaponLevel].speed * Time.deltaTime, 0f);
        if (transform.position.x > 11)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asrtroid asrtroid = collision.gameObject.GetComponent<Asrtroid>();
            if (asrtroid) asrtroid.TakeDamage(weapon.stats[weapon.weaponLevel].damage, true);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Boss")) 
        {
            Boss1 boss1 = collision.gameObject.GetComponent<Boss1>();
            if(boss1) boss1.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Critter"))
        {
            gameObject.SetActive(false);
        }

    }
}


