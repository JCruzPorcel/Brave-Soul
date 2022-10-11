using UnityEngine;

public class PlayerCombat : Singleton<PlayerCombat>
{
    GameObject[] enemies;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            MostNearbyEnemies().GetComponent<EnemyController>().TakeDamage(100);
        }
        else if (Input.GetKey(KeyCode.Mouse4))
        {
            MostNearbyEnemies().GetComponent<EnemyController>().TakeDamage(100);
        }
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
