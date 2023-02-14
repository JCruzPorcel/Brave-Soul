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

        ChangeAnimation(layerName);
    }

    private void Update()
    {
        ChangeAnimation(layerName);

        if (!overlayInWorld) return;


        if (GameManager.Instance.currentGameState != GameState.inGame)
        {
            animator.StartPlayback();
            return;
        }

        animator.StopPlayback();
    }

    public void ChangeAnimation(string name)
    {
        layerName = name;

        int layerIndex = animator.GetLayerIndex(name);
        int layerCount = animator.layerCount;

        if (!string.IsNullOrEmpty(name))
        {

            for (int i = 0; i < layerCount; i++)
            {
                if (i == layerIndex)
                {
                    animator.SetLayerWeight(i, 1f);
                }
                else
                {
                    animator.SetLayerWeight(i, 0f);
                }
            }
        }
        else
        {
            Debug.LogError("Invalid animation layer name");
        }
    }
}
