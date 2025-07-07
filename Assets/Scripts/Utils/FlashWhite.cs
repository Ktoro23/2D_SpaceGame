using UnityEngine;
using System.Collections;

public class FlashWhite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Material defaultMaterial;
    private Material whiteMaterial;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        whiteMaterial = Resources.Load<Material>("Materials/mWhite");
    }

    public void Flash()
    {
        spriteRenderer.material = whiteMaterial;
        StartCoroutine("ResetMaterial");
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = defaultMaterial;
    }

}
