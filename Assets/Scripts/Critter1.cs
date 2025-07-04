using UnityEditor.U2D.Sprites;
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
            moveInterval = Random.Range(0.1f, 2f);
            moveTimer = moveInterval;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 relativePos = targetPosition - transform.position;
        if(relativePos !=  Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1080 * Time.deltaTime);
        }

        float moveX = (GameManger.Instance.worldSpeed * PlayerMovement.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);

        if (transform.position.x < -11)
        {
            Destroy(gameObject);
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
            Instantiate(zappedEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            SoundsFXManager.Instance.PlayRandomSoundFXClip(flash, transform, 1f);
            Instantiate(burnEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
