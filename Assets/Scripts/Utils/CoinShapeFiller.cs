using UnityEngine;
using System.Collections;
public class CoinShapeFiller : MonoBehaviour
{
    [SerializeField] GameObject[] coinSlots;
    private void OnEnable()
    {
        StartCoroutine(NewMethod());
    }

    IEnumerator NewMethod()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject slot in coinSlots)
        {
            PoolHelper.GetPool(PoolTypes.Coin).GetPooledObject(slot.transform.position, slot.transform.rotation);
        }
    }
}
