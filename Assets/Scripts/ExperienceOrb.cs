using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public float followRange = 5f;
    public float followSpeed = 5f;
    public int gainExp;
    public Transform player;


    private bool isFollowing = false;

    PlayerController playerController;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame) return;


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

            playerController.TakeExp(gainExp);

            audioManager.Play("PickUpExp SFX");

            transform.position = transform.parent.position;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }

    }
    public void SetOrbExp(int min_exp, int max_exp)
    {
        gainExp = Random.Range(min_exp, min_exp);
    }

    private void OnDisable()
    {
        isFollowing = false;
    }
}
