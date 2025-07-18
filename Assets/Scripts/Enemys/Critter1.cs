
using UnityEngine;

public class Critter1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    private float moveTimer;
    private float moveInterval;

    private float moveSpeed;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    [SerializeField] private GameObject zappedEffect;
    [SerializeField] private GameObject burnEffect;

    [SerializeField] private AudioClip[] flash;
    [SerializeField] private AudioClip[] burn;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)]; 
        moveSpeed = Random.Range(0.5f, 3f);
        GenerateRandomPosition();
        moveInterval = Random.Range(0.5f, 2f);
        moveTimer = moveInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
        }
        else
        {
            GenerateRandomPosition();
            moveInterval = Random.Range(0.3f, 2f);
            moveTimer = moveInterval;
        }
        targetPosition -= new Vector3(GameManger.Instance.worldSpeed * Time.deltaTime, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 relativePos = targetPosition - transform.position;
        if(relativePos !=  Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1080 * Time.deltaTime);
        }

    }

    private void GenerateRandomPosition()
    {
        float randomX = Random.Range(-5f, 5f);
        float randomY = Random.Range(-5f, 5f);
        targetPosition = new Vector2(randomX, randomY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            SoundsFXManager.Instance.PlayRandomSoundFXClip(flash, transform, 1f);
            GameObject effect = PoolHelper.GetPool(PoolTypes.Critter1_Zapped).GetPooledObject(transform.position, transform.rotation);
            gameObject.SetActive(false);
            GameManger.Instance.critterCount++;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            SoundsFXManager.Instance.PlayRandomSoundFXClip(flash, transform, 1f);
            GameObject effect = PoolHelper.GetPool(PoolTypes.Critter1_Burn).GetPooledObject(transform.position, transform.rotation);
            gameObject.SetActive(false);
            GameManger.Instance.critterCount++;
        }
        
    }
}
