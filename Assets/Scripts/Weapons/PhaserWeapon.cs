using UnityEngine;

public class PhaserWeapon : MonoBehaviour
{
    public static PhaserWeapon Instance;

    //[SerializeField] private GameObject prefab;
    [SerializeField] private ObjectPooler bulletPool;
    [SerializeField] private AudioClip[] bulletsSFX;

    public float speed;
    public int damege;


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
        // Instantiate(prefab, transform.position, transform.rotation);
        GameObject bullet = bulletPool.GetPooledObject();
        SoundsFXManager.Instance.PlayRandomSoundFXClip(bulletsSFX, transform, 1f);
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }

}
