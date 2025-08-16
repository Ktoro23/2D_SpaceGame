using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Locustmorph : Enemy
{

    [SerializeField] private List<Frames> frames;
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
        spriteRenderer.sprite = frames[0].sprites[0];
    }

    private void EnterCharge()
    {
        spriteRenderer.sprite = frames[0].sprites[1];
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    

    

    [System.Serializable]
    private class Frames
    {
        public Sprite[] sprites;
    }
}
