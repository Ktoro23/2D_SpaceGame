using UnityEngine;
using System.Collections;

public class DestroyWhenAnimFinish : MonoBehaviour
{
    private Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(nameof(Deactivate));
    }


    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
