using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;

    Transform player;
    int armorPen;

    private void Start()
    {
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
