using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Enemy : MonoBehaviour
{
    [Range(0, 10)] public float damage;

    [Min(0)] public int maxHP;
    [Min(0)][SerializeField] float currentHP;

    [Range(0, 10)] public float speed;

    [Min(0)] public int min_exp;
    [Min(0)] public int max_exp;

    [HideInInspector] public bool isDead;
    public bool is_a_Boss;
    public bool reset;
    public Rigidbody2D rb;

    float xDir;
    float yDir;

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

        rb = GetComponent<Rigidbody2D>();

        Spawn();
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame)
        {
            animator.speed = 0;
            return;
        }

        animator.speed = 1;
        DespawnDistance();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame)
        {
            animator.speed = 0;
            rb.simulated = false;
            return;
        }

        rb.simulated = true;

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

      /*bool isBelowPlayerY = transform.position.y < player.position.y + 0.03f;
        bool isAbovePlayerY = transform.position.y > player.position.y - 0.03f;
        bool isLeftOfPlayerX = transform.position.x < player.position.x + 0.03f;
        bool isRightOfPlayerX = transform.position.x > player.position.x - 0.03f;*/
        /*switch ((isBelowPlayerY, isAbovePlayerY, isLeftOfPlayerX, isRightOfPlayerX))
        {
            case (true, false, _, _):
                sr.sortingOrder = 1;
                break;
            case (false, true, _, _):
                sr.sortingOrder = -1;
                break;
            case (_, _, true, false):
                sr.flipX = false;
                break;
            case (_, _, false, true):
                sr.flipX = true;
                break;
            default:
                break;
        }*/

        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    public virtual void CurrentHealth()
    {
        if (is_a_Boss)
        {
            animator.SetTrigger("Hit");
        }


        if (currentHP <= 0)
        {
            isDead = true;

            GiveExp();


            if (is_a_Boss)
            {
                animator.SetBool("isDead", true);
            }
            else
            {
                reset = true;
            }
            Death();
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
        Vector2 delta = transform.position - player.position;

        float xThreshold = 15;
        float yThreshold = 12;

        if (Mathf.Abs(delta.x) > xThreshold || Mathf.Abs(delta.y) > yThreshold || reset)
        {
            float randomX = Random.Range(0f, 1f);


            if (randomX < .5f)
            {
                yDir = Random.Range(0f, 1f) < .5f ? -yThreshold : yThreshold;
                xDir = Random.Range(-xThreshold, xThreshold);
            }
            else
            {
                xDir = Random.Range(0f, 1f) < .5f ? -xThreshold : xThreshold;
                yDir = Random.Range(-yThreshold, xThreshold);
            }

            transform.position = new Vector2(player.position.x + xDir, player.position.y + yDir);

            if (reset)
            {
                currentHP = maxHP;
                isDead = false;
                reset = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D player)
    {
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            if (!isDead)
            {
                if (player.CompareTag("Character"))
                {
                    PlayerController.Instance.TakeDamage(damage);
                }
            }
        }
    }

    public virtual void Attack() { }

    public virtual void Spawn() { }


    public virtual void GiveExp() { ExperienceOrbPooling.Instance.OnEnemyDeath(this); timerScript.enemiesKilled++; }

    public virtual void Death() { }
}
