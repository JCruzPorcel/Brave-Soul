using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [Range(0, 10)] public float damage;

    [Min(0)] public int maxHP;
    [Min(0)] float currentHP;

    [Range(0, 10)] public float speed;

    [Min(0)] public int min_exp;
    [Min(0)] public int max_exp;

    [HideInInspector] public bool isDead;
    public bool is_a_Boss;
    public bool reset;

    int xDir;
    int yDir;

    [HideInInspector] public Animator animator;

    [HideInInspector] public SpriteRenderer sr;

    public Transform player;

    TimerScript timerScript;

    public GameObject floatingText;

    [HideInInspector] public AudioManager sourceManager;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;

        animator = GetComponent<Animator>();

        timerScript = FindObjectOfType<TimerScript>();
        currentHP = maxHP;

        sourceManager = FindObjectOfType<AudioManager>();

        Spawn();
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame)
        {
            animator.speed = 0;
            return;
        }

        if (PlayerController.Instance.currentLvl >= 7 && PlayerController.Instance.currentLvl <= 16)
        {
            min_exp *= 2;
            max_exp *= 2;
        }
        else if (PlayerController.Instance.currentLvl >= 17)
        {
            min_exp *= 3;
            max_exp *= 3;
        }


        animator.speed = 1;
        DespawnDistance();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame)
        {
            animator.speed = 0;
            return;
        }

        animator.speed = 1;

        if (!isDead)
        {
            Movement();
            Attack();
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

        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    public virtual void CurrentHealth()
    {
        if (currentHP <= 0)
        {
            isDead = true;

            GiveExp();

            reset = true;

            if (is_a_Boss)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ShowDamage(int damage)
    {
        GameObject go = Instantiate(floatingText);
        go.transform.position = new Vector2(transform.position.x + Random.Range(0f, .5f), transform.position.y + Random.Range(.5f, 1f));

        if (damage < 35)
        {
            go.GetComponent<TMP_Text>().color = Color.white;
        }
        else if (damage >= 35)
        {
            go.GetComponent<TMP_Text>().color = Color.yellow;
        }

        go.GetComponent<TMP_Text>().text = damage.ToString();

        Destroy(go, .50f);
    }

    public virtual void TakeDamage(int playerDamage)
    {
        if (GameManager.Instance.GM_ShowDamage)
        {
            ShowDamage(playerDamage);
        }
        currentHP -= playerDamage;
        CurrentHealth();
    }

    private void DespawnDistance()
    {
        Vector2 delta = transform.position - player.transform.position;

        if (Mathf.Abs(delta.x) > 12 || Mathf.Abs(delta.y) > 7)
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

    public virtual void OnTriggerStay2D(Collider2D col)
    {
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            if (!isDead)
            {
                if (col.tag == "Character")
                {
                    PlayerController.Instance.TakeDamage(damage);
                }
            }
        }
    }

    public virtual void Attack() { }

    public virtual void Spawn() { }


    public virtual void GiveExp() { ExperienceOrbPooling.Instance.OnEnemyDeath(this); timerScript.enemiesKilled++; }
}
