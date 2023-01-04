using UnityEngine;

public class Axe : Weapon
{
    Rigidbody2D rb;
    bool onEnter;
    [SerializeField] int enemyPen = 0;
    public int amount = 3;

    int rr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                WeaponContainer.Instance.attackSpeedAxe = attackSpeed;
            }
        }

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            if (onEnter)
            {
                transform.position = player.position;

                rb.velocity = (transform.up * Random.Range(450, 500)) * Time.fixedDeltaTime;

                onEnter = false;
            }

            if (rr == 1)
            {
                transform.Rotate(Vector3.forward, -450 * Time.fixedDeltaTime);
            }
            else
            {
                transform.Rotate(Vector3.forward, 450 * Time.fixedDeltaTime);
            }

            DespawnRange();

        }
    }

    private void OnDisable()
    {
        WeaponLevel();
    }

    private void OnEnable()
    {
        int rx = Random.Range(0, 2);
        rr = rx;
        rx = rx == 1 ? -7 : 7;

        Vector3 rotation = new Vector3(0f, 0f, rx);
        transform.eulerAngles = rotation;
        onEnter = true;
        WeaponLevel();
    }

    void DespawnRange()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) > 12 || Mathf.Abs(transform.position.y - player.position.y) > 10)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);

            if (enemyPen == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                enemyPen--;
            }
        }
    }


    public void WeaponLevel()
    {
        level = GetStatsManager.Instance.level_Axe;

        switch (level)
        {
            case 1:
                damage = 10;
                attackSpeed = 4.2f;
                amount = 3;
                enemyPen = 0;
                break;

            case 2:
                damage = 15;
                attackSpeed = 3f;
                amount = 4;
                enemyPen = 0;
                break;

            case 3:
                damage = 30;
                attackSpeed = 2f;
                amount = 4;
                enemyPen = 1;
                break;

            case 4:
                damage = 50;
                attackSpeed = 1.5f;
                amount = 5;
                enemyPen = 2;
                break;

            case 5:
                damage = 75;
                attackSpeed = 1f;
                amount = 7;
                enemyPen = 3;
                break;
        }
    }
}
