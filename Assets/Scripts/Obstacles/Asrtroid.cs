using UnityEngine;
using System.Collections;

public class Asrtroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject desroyEffect;
    [SerializeField] private int lives;

    private Rigidbody2D rb;

    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;

    [SerializeField] AudioClip[] rockHit;

    [SerializeField] private Sprite[] sprites;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); 
        defaultMaterial = spriteRenderer.material;
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1);

        rb.linearVelocity = new Vector2(pushX, pushY);

        float randomScale = Random.Range(0.4f, 1f);
        transform.localScale = new Vector2(randomScale, randomScale);
       
    }

    
    void Update()
    {
        float moveX = (GameManger.Instance.worldSpeed * PlayerMovement.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if(transform.position.x < -11)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"));
        {
            spriteRenderer.material = whiteMaterial;
            StartCoroutine("ResetMaterial");
            SoundsFXManager.Instance.PlayRandomSoundFXClip(rockHit, transform, 1f);
            lives--;
            if(lives <= 0)
            {
                Instantiate(desroyEffect, transform.position, transform.rotation);
                SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.boom2, 1f);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = defaultMaterial;
    }
}
