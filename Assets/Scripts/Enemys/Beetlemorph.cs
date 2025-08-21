using UnityEngine;

public class Beetlemorph : Enemy
{
    [SerializeField] private Sprite[] sprites;
    private float timer;
    private float frequency;
    private float amplitude;
    private float centerY;

    public override void OnEnable()
    {
        base.OnEnable();
        timer = 0;
        frequency = Random.Range(0.3f, 1f);
        amplitude = Random.Range(0.8f, 1.5f);
        centerY = transform.position.y;
    }

    public override void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
        hitSound = SoundsFXManager.Instance.bettleHit;
        destroySound = SoundsFXManager.Instance.beetleDestroy;
        speedX = Random.Range(-0.8f, -1.5f);
    }

    public override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;
        float sine = Mathf.Sin(timer * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, centerY + sine);
    }
    protected override void PlayDeathAnim()
    {
        
        GameObject effect = PoolHelper.GetPool(PoolTypes.BeetlePop).GetPooledObject(transform.position, transform.rotation);

    }
}
