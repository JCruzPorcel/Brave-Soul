using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectileData projectileData;

    GameObject[] enemies;
    Rigidbody2D rb;
    Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        transform.position += (transform.up * projectileData.Speed) * Time.fixedDeltaTime;
        DisableGO();
    }

    private void OnEnable()
    {
        Transform target = MostNearbyEnemies();
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, -angle);
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
}
