 using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[Header("Basic Stats")]
    float moveSpeed;
    int damage;
    int maxHealth;
    [Min(0)]float currentHealth;
    int exp;
    [SerializeField] EnemyData enemyData;

    Transform player;

    bool reset;
    bool isDead = false;

    int yDir;
    int xDir;

    SpriteRenderer sr;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        sr = GetComponent<SpriteRenderer>();

        maxHealth = enemyData.hp;
        moveSpeed = enemyData.speed;
        damage = enemyData.damage;
        exp = enemyData.exp;

        currentHealth = maxHealth;
    }


    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame) return;


        CurrentHp();

        while (reset)
        {
            //Vector2 playerDir = new Vector2(player.position.x, player.position.y);


            int randomX = Random.Range(0, 2);

            if (randomX == 0)
            {
                yDir = Random.Range(0, 2) == 0 ? -6 : 6;
                xDir = Random.Range(-11, 11);
            }
            else if (randomX == 1)
            {
                xDir = Random.Range(0, 2) == 0 ? -11 : 11;
                yDir = Random.Range(-6, 6);
            }

            transform.position = new Vector2(player.position.x + xDir, player.position.y + yDir);

            currentHealth = maxHealth;
            isDead = false;
            gameObject.SetActive(true);

            reset = false;
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) > 12)
        {
            reset = true;
        }

        if (Mathf.Abs(transform.position.y - player.transform.position.y) > 7)
        {
            reset = true;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame) return;

        if (!isDead)
        {
            Movement();
        }
    }

    void Movement()
    {
        Vector3 direction = player.position - transform.position;

        direction.Normalize();

        if (transform.position.y < player.position.y + .03f)
        {
            sr.sortingOrder = 1;

        }
        else if (transform.position.y > player.position.y - .03f)
        {
            sr.sortingOrder = -1;
        }

        if (transform.position.x < player.position.x + .03f)
        {
            sr.flipX = false;
        }
        else if (transform.position.x > player.position.x - .03f)
        {
            sr.flipX = true;
        }

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    void CurrentHp()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            currentHealth = 0;
            PlayerController.Instance.TakeExp(exp);
            gameObject.SetActive(false);
            reset = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!isDead)
        {
            if (col.tag == "Player")
            {
                PlayerController.Instance.TakeDamage(damage);
            }
        }
    }
}
