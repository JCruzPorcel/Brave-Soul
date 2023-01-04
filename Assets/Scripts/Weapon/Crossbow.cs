using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    Transform container;
    GameObject[] enemies;
    [SerializeField] List<GameObject> arrows = new List<GameObject>();
    [SerializeField] WeaponData arrowData;


    float timerPlus;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        container = GameObject.Find("Container").transform;

        transform.position = new Vector2(player.position.x, player.position.y + .25f);

        for (int i = 0; i < 75; i++)
        {
            GameObject go = Instantiate(arrowData.Prefab, player);
            go.transform.SetParent(container);
            go.SetActive(false);
            arrows.Add(go);
        }

        transform.SetParent(player);

        PlayerCombat.Instance.sliderBar.MaxAttackSpeed(attackSpeed);
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {

            if (!PlayerController.Instance.IsDead)
            {
                WeaponLevel();

                PlayerCombat.Instance.sliderBar.MaxAttackSpeed(attackSpeed);

                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    timer = attackSpeed;
                }


                if (timerPlus < attackSpeed)
                {
                    timerPlus += Time.deltaTime;
                    PlayerCombat.Instance.sliderBar.NextAttack(timerPlus);
                }
                else
                {
                    timerPlus = 0;
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
        }
    }

    void Arrow(GameObject go)
    {
        if (timer > 0 || MostNearbyEnemies() == null)
            return;

        Vector3 eulerRotation = new Vector3(0, 0, transform.eulerAngles.z - 45);

        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(eulerRotation);

        go.SetActive(true);
        timer = attackSpeed;
    }

    private void RotationMode()
    {
        if (MostNearbyEnemies() == null)
            return;

        Transform target = MostNearbyEnemies();
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg - 45;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -angle), 10 * Time.deltaTime);
    }

    public Transform MostNearbyEnemies()
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

    public void WeaponLevel()
    {
        level = GetStatsManager.Instance.level_Crossbow;

        switch (level)
        {
            case 1:
                attackSpeed = 4.5f;
                break;

            case 2:
                attackSpeed = 4f;
                break;

            case 3:
                attackSpeed = 3f;
                break;

            case 4:
                attackSpeed = 1.5f;
                break;

            case 5:
                attackSpeed = 1f;
                break;
        }
    }

}
