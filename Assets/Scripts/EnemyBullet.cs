using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage;
    public float speed;
    public Transform player;
    Vector3 direction;

    public GameObject secondBulletSprite;
    [HideInInspector] public Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            transform.position += direction * speed * Time.fixedDeltaTime;

            DespawnDistance();
        }
    }


    void DespawnDistance()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) > 12 || Mathf.Abs(transform.position.y - player.position.y) > 10)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        player = GameObject.Find("Player").transform;

        direction = (player.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Character")
        {
            animator.SetBool("Hit", true);
            other.GetComponentInParent<PlayerController>().TakeDamage(damage);
        }
    }
}
