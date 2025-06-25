using UnityEngine;
using System.Collections;

public class Asrtroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[] sprites;
    private Rigidbody2D rb;

    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); 
        defaultMaterial = spriteRenderer.material;
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1);

        rb.linearVelocity = new Vector2(pushX, pushY);
       
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
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = defaultMaterial;
    }
}
