using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [Range(0, 10)] public int damage;

    [Min(0)] public int maxHP;
    [Min(0)] float currentHP;

    [Range(0, 10)] public float speed;

    [Min(0)] public int exp;

    [HideInInspector] public bool isDead;
    public bool is_a_Boss;
    public bool reset;

    int xDir;
    int yDir;

    [HideInInspector] public Animator animator;

    SpriteRenderer sr;

    public Transform player;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;

        currentHP = maxHP;
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame) return;

        DespawnDistance();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame) return;

        if (!isDead)
        {
            Movement();
        }
    }


    public virtual void Movement()
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

        transform.position += direction * speed * Time.deltaTime;
    }

    public virtual void CurrentHealth()
    {
        if (currentHP <= 0)
        {
            isDead = true;

            if (!is_a_Boss)
            {
                reset = true;
            }else
            {
               gameObject.SetActive(false);
            }

            GiveExp();
        }
    }

    public virtual void TakeDamage(int playerDamage)
    {
        currentHP -= playerDamage;
        CurrentHealth();
    }

    private void DespawnDistance()
    {
        if (Mathf.Abs(transform.position.y - player.transform.position.y) > 7 ||
           (Mathf.Abs(transform.position.x - player.transform.position.x) > 12))
        {
            int randomX = Random.Range(0, 2);


            if (randomX == 0)
            {
                yDir = Random.Range(0, 2) == 0 ? -6 : 6;
                xDir = Random.Range(-11, 11);
            }
            else
            {
                xDir = Random.Range(0, 2) == 0 ? -11 : 11;
                yDir = Random.Range(-6, 6);
            }

            transform.position = new Vector2(player.position.x + xDir, player.position.y + yDir);
        }


        if (reset)
        {
            int randomX = Random.Range(0, 2);


            if (randomX == 0)
            {
                yDir = Random.Range(0, 2) == 0 ? -6 : 6;
                xDir = Random.Range(-11, 11);
            }
            else
            {
                xDir = Random.Range(0, 2) == 0 ? -11 : 11;
                yDir = Random.Range(-6, 6);
            }

            transform.position = new Vector2(player.position.x + xDir, player.position.y + yDir);

            currentHP = maxHP;

            isDead = false;

            reset = false;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!isDead)
        {
            if (col.tag == "Character")
            {
                PlayerController.Instance.TakeDamage(damage);
            }
        }
    }



    public virtual void Attack() { }

    public virtual void Spawn() { }

    public virtual void GiveExp() { PlayerController.Instance.TakeExp(exp); }
}
