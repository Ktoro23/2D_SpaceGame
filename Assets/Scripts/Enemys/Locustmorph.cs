using UnityEngine;

public class Locustmorph : Enemy
{
    public override void Start()
    {
        base.Start();
        hitSound = SoundsFXManager.Instance.bettleHit;
        destroySound = SoundsFXManager.Instance.beetleDestroy;
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
}
