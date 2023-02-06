using UnityEngine;

public class ChangeAnimatorLayer : MonoBehaviour
{
    public Animator animator;
    public string layerName;
    public bool overlayInWorld;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        int layerIndex = animator.GetLayerIndex(layerName);

        animator.SetLayerWeight(layerIndex, 1f);
    }

    private void Update()
    {
        int layerIndex = animator.GetLayerIndex(layerName);

        animator.SetLayerWeight(layerIndex, 1f);

        if (!overlayInWorld) return;


        if (GameManager.Instance.currentGameState != GameState.inGame)
        {

            animator.StartPlayback();
            return;
        }

        animator.StopPlayback();
    }
}
