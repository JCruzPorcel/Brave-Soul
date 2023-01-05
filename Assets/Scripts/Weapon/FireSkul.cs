using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkul : Weapon
{

    GameObject[] enemies;

    private void Update()
    {
        Transform target = MostNearbyEnemies();

        transform.position += target.position;
    }


    Transform MostNearbyEnemies()
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
