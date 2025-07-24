using UnityEngine;

public class PhaserWeapon : Weapon
{
    public static PhaserWeapon Instance;

    [SerializeField] private ObjectPooler bulletPool;
    [SerializeField] private AudioClip[] bulletsSFX;

    


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


    public void Shoot()
    {
        SoundsFXManager.Instance.PlayRandomSoundFXClip(bulletsSFX, transform, 1f);

        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = bulletPool.GetPooledObject();
            float yPos = transform.position.y;
            if (amount > 1)
            {
                float spacing = range / (amount - 1);
                yPos = transform.position.y - (range / 2) + i * spacing;
            }
            
            bullet.transform.position = new Vector2(transform.position.x, yPos);
            bullet.transform.localScale = new Vector2(size, size);
            bullet.SetActive(true);
        }
        
    }

}
