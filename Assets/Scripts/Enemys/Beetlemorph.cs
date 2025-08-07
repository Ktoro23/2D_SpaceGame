using UnityEngine;

public class Beetlemorph : Enemy
{
    [SerializeField] private Sprite[] sprites;

    public override void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
    }
}
