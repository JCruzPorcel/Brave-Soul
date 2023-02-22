using UnityEngine;

public class PopMessageController : Singleton<PopMessageController>
{
    public Animator animator;
    public GameObject messagePrefab;

    private void Awake()
    {
        animator.SetBool("Start", true);
    }
}
