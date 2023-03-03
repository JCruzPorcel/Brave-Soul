using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    Transform container;
    GameObject[] enemies;
    [SerializeField] List<GameObject> arrows = new List<GameObject>();
    Queue<GameObject> arrowQueue = new Queue<GameObject>();
    [SerializeField] WeaponData arrowData;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        container = GameObject.Find("Container").transform;

        transform.position = new Vector2(player.position.x, player.position.y + .25f);

        for (int i = 0; i < 25; i++)
        {
            GameObject go = Instantiate(arrowData.Prefab, player);
            go.transform.SetParent(container);
            go.SetActive(false);
            arrows.Add(go);
        }

        transform.SetParent(player);

        WeaponLevel();
        PlayerCombat.Instance.sliderBar.MaxAttackSpeed(attackSpeed);
    }

    private void Update()
    {
        WeaponLevel();

        if (GameManager.Instance.currentGameState == GameState.inGame)
        {

            if (!PlayerController.Instance.IsDead)
            {
                if (MostNearbyEnemies() == null) return;


                PlayerCombat.Instance.sliderBar.MaxAttackSpeed(attackSpeed);

                if (timer < attackSpeed)
                {
                    timer += Time.deltaTime;
                    PlayerCombat.Instance.sliderBar.NextAttack(timer);
                }
                else
                {
                    Arrow();
                    timer = 0;
                }

                Rotation();

                foreach (GameObject go in arrows)
                {
                    if (!go.activeInHierarchy)
                    {
                        go.GetComponent<Projectile>().WeaponLevel();

                        arrowQueue.Enqueue(go);
                    }
                }
            }
        }
    }

    void Arrow()
    {
        if (MostNearbyEnemies() == null) return;

        GameObject go = arrowQueue.Peek();

        Vector3 eulerRotation = new Vector3(0, 0, transform.eulerAngles.z - 45);

        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(eulerRotation);

        FindObjectOfType<AudioManager>().Play("Arrow SFX");

        go.GetComponent<Projectile>().WeaponLevel();
        go.SetActive(true);
        arrowQueue.Dequeue();
    }

    private void Rotation()
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

    public override void WeaponLevel()
    {
        level = LevelUpManager.Instance.level_Crossbow;
        texts = LanguageManager.Instance.texts;

        weaponData.ItemName = texts[weaponData.ID];


        if(level <= -1)
        {
            weaponData.Description = texts[weaponData.ID + 1];
        }
        else if (level <= 5)
        {
            weaponData.Description = texts[weaponData.ID + level + 2];
        }

        switch (level)
        {
            case -1:
                attackSpeed = 1.3f;
                break;

            case 0:
                attackSpeed = 1.3f;
                break;

            case 1:
                attackSpeed = 1.2f;
                break;

            case 2:
                attackSpeed = 1f;
                break;

            case 3:
                attackSpeed = .75f;
                break;

            case 4:
                attackSpeed = .2f;
                break;

            case 5:
                attackSpeed = .1f;
                break;
        }
    }
}
