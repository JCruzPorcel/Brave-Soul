using UnityEngine;

public class Necronomicon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    Transform player;

    //Stats
    int weaponLevel;
    int damage;
    float attackSpeed;
    float weaponSpin;
    float activeTime;
    int amountWeapon;


    private void Start()
    {
        GetStats(weaponData);

        player = GameObject.Find("Player").transform;
        transform.position = new Vector2(player.position.x, transform.position.y + 1);
    }

    private void Update()
    {
        Level();

        transform.Rotate(0, 0, 40 * Time.deltaTime);
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, -1), 40 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }

    void GetStats(WeaponData weaponData)
    {
        damage = weaponData.Damage;
        attackSpeed = weaponData.AttackSpeed;
        weaponLevel = weaponData.Level;
        amountWeapon = weaponData.AmountWeapon;
    }

    void Level()
    {
        switch (weaponLevel)
        {
            case 2:
                damage += 10;
                break;
            case 3:
                attackSpeed = .75f;
                break;
            case 4:
                damage += 7;
                attackSpeed = .50f;
                break;
            case 5:
                damage += 12;
                attackSpeed = .25f;
                //evolution true
                break;
            default:
                break;
        }
    }
}
