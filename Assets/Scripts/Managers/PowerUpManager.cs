using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    [Header("Bullet Appearance")]
    public Sprite normalBulletSprite;
    public Sprite poweredBulletSprite;

    [Header("Power-Up Settings")]
    public int extraDamage = 5;       // damage added on top of weapon stats
    public float duration = 10f;

    public bool IsPoweredUp { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ActivatePowerUp()
    {
        if (!IsPoweredUp)
            StartCoroutine(PowerUpRoutine());
    }

    private IEnumerator PowerUpRoutine()
    {
        IsPoweredUp = true;

        // Update all active bullets in the scene
        foreach (PhaserBullet bullet in FindObjectsOfType<PhaserBullet>())
        {
            bullet.UpdateSprite();
        }

        yield return new WaitForSeconds(duration);

        IsPoweredUp = false;

        // Revert sprites
        foreach (PhaserBullet bullet in FindObjectsOfType<PhaserBullet>())
        {
            bullet.UpdateSprite();
        }
    }
}

