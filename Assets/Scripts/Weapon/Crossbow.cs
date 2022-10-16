using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;

    Transform player;
    Transform container;
    GameObject[] enemies;
    [SerializeField] List<GameObject> arrows = new List<GameObject>();
    [Min(0)] float timer;
    [SerializeField] float modify;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        container = GameObject.Find("Container").transform;
        timer = weaponData.AttackSpeed;

        for (int i = 0; i < weaponData.ProjectileType.ProjectileAmount; i++)
        {
            GameObject go = Instantiate(weaponData.ProjectileType.Prefab, player);
            go.transform.SetParent(container);
            go.SetActive(false);
            arrows.Add(go);
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        RotationMode();

        foreach (GameObject go in arrows)
        {
            if (!go.activeInHierarchy)
            {
                Arrow(go);
            }
        }
    }

    void Arrow(GameObject go)
    {
        if (timer > 0)
            return;

        Vector3 eulerRotation = new Vector3(0, 0, transform.eulerAngles.z - 45);

        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(eulerRotation);

        go.SetActive(true);
        timer = weaponData.AttackSpeed;
    }

    private void RotationMode()
    {
        if (MostNearbyEnemies() == null)
            return;

        Transform target = MostNearbyEnemies();
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg - 45;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -angle), 7 * Time.deltaTime);
    }

    private Transform MostNearbyEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
            return null;


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
