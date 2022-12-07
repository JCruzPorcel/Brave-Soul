using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    Rigidbody2D rb;
    bool onEnter;
    Transform player;
    int rr;

    //Stats
    int weaponLevel;
    int damage;
    float attackSpeed;
    int weaponPen;
    int amountWeapon;

    float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        GetStats(weaponData);
        timer = attackSpeed;
    }
    private void Update()
    {
        Level();
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        
        if (timer <= 0)
        {
            for (int i = 0; i < amountWeapon; i++)
            {
                GameObject go = Instantiate(weaponData.Prefab, player);
                go.transform.position = new Vector3(player.position.x, player.position.y + .2f);
            }
            timer = attackSpeed;
        }
    }

    private void FixedUpdate()
    {


        if (onEnter)
        {
            rb.velocity = (transform.up * 500) * Time.fixedDeltaTime;
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

        DisableGO();
    }

    #region Disable

    private void OnEnable()
    {
        int rx = Random.Range(0, 2);
        rr = rx;
        rx = rx == 1 ? -7 : 7;

        Vector3 rotation = new Vector3(0f, 0f, rx);
        transform.eulerAngles = rotation;
        onEnter = true;
    }

    void DisableGO()
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
            collision.GetComponent<EnemyController>().TakeDamage(damage);

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
    #endregion

    void GetStats(WeaponData weaponData)
    {
        damage = weaponData.Damage;
        attackSpeed = weaponData.AttackSpeed;
        weaponLevel = weaponData.Level;
        weaponPen = weaponData.ArmorPen;
        amountWeapon = weaponData.Amount;
    }

    void Level()
    {
        switch (weaponLevel)
        {
            case 2:
                damage = 45;
                weaponPen = 1;
                break;
            case 3:
                attackSpeed = .75f;
                amountWeapon = 4;
                break;
            case 4:
                damage = 75;
                attackSpeed = .50f;
                amountWeapon = 5;
                break;
            case 5:
                damage = 120;
                weaponPen = 3;
                attackSpeed = .25f;
                //evolution true
                break;
            default:
                break;
        }
    }
}