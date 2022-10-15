using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;

    GameObject[] enemies;
    Rigidbody2D rb;
    Transform player;
    int armorPen;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        transform.position += (transform.up * weaponData.ProjectileType.Speed) * Time.fixedDeltaTime;
        DisableGO();
    }

    private void OnEnable()
    {
        armorPen = weaponData.ProjectileType.ArmorPen;
    }

    private Transform MostNearbyEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nearbyEnemy = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in enemies)
        {
            float currentDistance;
            currentDistance = Vector2.Distance(transform.position, go.transform.position);

            if (currentDistance < nearbyEnemy)
            {
                nearbyEnemy = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }

    void DisableGO()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) > 12 || Mathf.Abs(transform.position.y - player.position.y) > 10)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            if (armorPen == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                armorPen--;
            }

            other.GetComponent<EnemyController>().TakeDamage(weaponData.Damage);
        }
    }
}
