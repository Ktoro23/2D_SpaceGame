using UnityEngine;

public class DestroyWhenAnimFinish : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
