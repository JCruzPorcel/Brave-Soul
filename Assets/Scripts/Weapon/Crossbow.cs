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

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        container = GameObject.Find("Container").transform;

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

        for (int i = 0; i < arrows.Count && timer <= 0; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                arrows[i].transform.position = player.position;
                arrows[i].SetActive(true);
            }
                timer = weaponData.AttackSpeed;
        }
    }

    private void RotationMode()
    {
        Transform target = MostNearbyEnemies();
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg - 35;
        transform.rotation = Quaternion.Euler(0, 0, -angle);
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
}
