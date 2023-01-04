using UnityEngine;

public class Axe : Weapon
{
    Rigidbody2D rb;
    bool onEnter;
    [SerializeField] int weaponPen = 0;
    public int weaponAmount = 3;
    int rr;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }


    private void Update()
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

    private void FixedUpdate()
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

    private void OnDisable()
    {
        WeaponContainer.Instance.attackSpeedAxe = attackSpeed;
    }

    private void OnEnable()
    {
        int rx = Random.Range(0, 2);
        rr = rx;
        rx = rx == 1 ? -7 : 7;

        Vector3 rotation = new Vector3(0f, 0f, rx);
        transform.eulerAngles = rotation;
        onEnter = true;
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

            if (weaponPen == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                weaponPen--;
            }
        }
    }
}
