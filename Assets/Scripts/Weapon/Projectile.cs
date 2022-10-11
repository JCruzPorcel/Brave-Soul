using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject[] enemies;
    Transform target;
    [SerializeField] WeaponData weaponData;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        target = MostNearbyEnemies();
        Debug.Log("SUCCESSFUL CREATION");
    }
    public Transform MostNearbyEnemies()
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
}
