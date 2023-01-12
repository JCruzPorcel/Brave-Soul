using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public float followRange = 5f;
    public float followSpeed = 5f;
    public int min_experienceGain = 0;
    public int max_experienceGain = 0;
    public Transform player;



    private bool isFollowing = false;

    PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= followRange && !isFollowing)
        {
            isFollowing = true;
        }

        if (isFollowing)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= 0.1f)
        {
            isFollowing = false;

            playerController.TakeExp(Random.Range(min_experienceGain, max_experienceGain));

            transform.position = transform.parent.position;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }

    }
    public void SetOrbExp(int min_exp, int max_exp)
    {
        min_experienceGain = min_exp;
        max_experienceGain = max_exp;
    }

    private void OnDisable()
    {
        isFollowing = false;
    }
}
