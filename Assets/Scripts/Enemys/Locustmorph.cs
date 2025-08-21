using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Locustmorph : Enemy
{

    [SerializeField] private List<Frames> frames;
    private int enemyVariant;
    private bool charging;

    public override void OnEnable()
    {
        base.OnEnable();
        enemyVariant = Random.Range(0, frames.Count);
        EnterIdle();

    }
    public override void Start()
    {
        base.Start();
        hitSound = SoundsFXManager.Instance.locustHit;
        destroySound = SoundsFXManager.Instance.locustDestroy;
        speedX = Random.Range(0.5f, 0.8f);
        speedY = Random.Range(-0.9f, 0.5f);
    }

    public override void Update()
    {
        base.Update();
        if(transform.position.y > 5 || transform.position.y < -5)
        {
            speedY *= -1;
        }
    }

    private void EnterIdle()
    {
        charging = false;
        spriteRenderer.sprite = frames[enemyVariant].sprites[0];
        speedX = Random.Range(0.5f, 0.8f);
        speedY = Random.Range(-0.9f, 0.5f);
    }

    private void EnterCharge()
    {
        if (!charging)
        {
            charging = true;
            spriteRenderer.sprite = frames[enemyVariant].sprites[1];
            SoundsFXManager.Instance.PlaySoundFXClip(SoundsFXManager.Instance.locustCharge, 1f);
            speedX = Random.Range(-4f, -6f);
            speedY = 0;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if(lives <= maxLives * 0.1)
        {
            EnterCharge();
        }
    }

    protected override void PlayDeathAnim()
    {
        
        GameObject effect = PoolHelper.GetPool(PoolTypes.LocustPop).GetPooledObject(transform.position, transform.rotation);

    }


    [System.Serializable]
    private class Frames
    {
        public Sprite[] sprites;
    }
}
