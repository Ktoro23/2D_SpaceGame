using UnityEngine;

public class PhaserBullet : MonoBehaviour
{

    PhaserWeapon weapon;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        weapon = PhaserWeapon.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {

        UpdateSprite();
    }
    private void Update()
    {
        transform.position += new Vector3(weapon.stats[weapon.weaponLevel].speed * Time.deltaTime, 0f);
        if (transform.position.x > 10.9)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int dmg = GetDamage();
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asrtroid asrtroid = collision.gameObject.GetComponent<Asrtroid>();
            if (asrtroid) asrtroid.TakeDamage(weapon.stats[weapon.weaponLevel].damage, true);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Critter"))
        {
            gameObject.SetActive(false);
        }
       
         else if (collision.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                    if (enemy) enemy.TakeDamage(dmg);
                    gameObject.SetActive(false);
                }

    }

    private int GetDamage()
    {
        // If power-up active, boost damage
        if (PowerUpManager.Instance != null && PowerUpManager.Instance.IsPoweredUp)
            return weapon.stats[weapon.weaponLevel].damage + PowerUpManager.Instance.extraDamage;

        return weapon.stats[weapon.weaponLevel].damage;
    }

    public void UpdateSprite()
    {
        if (PowerUpManager.Instance != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = PowerUpManager.Instance.IsPoweredUp ?
                PowerUpManager.Instance.poweredBulletSprite :
                PowerUpManager.Instance.normalBulletSprite;
        }
    }
}


