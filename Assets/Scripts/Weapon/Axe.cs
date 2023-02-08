using UnityEngine;

public class Axe : Weapon
{
    Rigidbody2D rb;
    bool onEnter;
    [SerializeField] int enemyPen = 0;
    public int amount = 2;
    int rr;

    private void Start()
    {
        WeaponLevel();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        WeaponLevel();

        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            rb.simulated = true;
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                WeaponContainer.Instance.attackSpeedAxe = attackSpeed;
            }
        }
        else
        {
            rb.simulated = false;
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

    private void OnEnable()
    {
        int rx = Random.Range(0, 2);
        rr = rx;
        rx = rx == 1 ? -7 : 7;

        Vector3 rotation = new Vector3(0f, 0f, rx);
        transform.eulerAngles = rotation;
        onEnter = true;
        FindObjectOfType<AudioManager>().Play("Axe SFX");
    }

    void DespawnRange()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) > 15 || Mathf.Abs(transform.position.y - player.position.y) > 12)
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


    public override void WeaponLevel()
    {
        level = LevelUpManager.Instance.level_Axe;
        texts = LanguageManager.Instance.texts;

        weaponData.ItemName = texts[weaponData.ID];


        if (level <= -1)
        {
            weaponData.Description = texts[weaponData.ID + 1];
        }
        else if (level <= 5)
        {
            weaponData.Description = texts[weaponData.ID + level + 2];
        }


        switch (level)
        {
            case -1:
                break;

            case 0:
                damage = 15;
                attackSpeed = 5f;
                amount = 2;
                enemyPen = 0;
                break;

            case 1:
                damage = 15;
                attackSpeed = 4.2f;
                amount = 2;
                enemyPen = 0;
                break;

            case 2:
                damage = 15;
                attackSpeed = 3f;
                amount = 3;
                enemyPen = 0;
                break;

            case 3:
                damage = 30;
                attackSpeed = 3f;
                amount = 3;
                enemyPen = 0;
                break;

            case 4:
                damage = 50;
                attackSpeed = 2f;
                amount = 3;
                enemyPen = 0;
                break;

            case 5:
                damage = 50;
                attackSpeed = 1.25f;
                amount = 5;
                enemyPen = 2;
                break;
        }
    }
}
