using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int amount;
    }

    public List<Pool> pools; // ALL PREFABS ON START

    public List<GameObject> enemyPool = new List<GameObject>(); //  NEW PREFABS CONTAINER

    static Dictionary<int, GameObject> objsPool = new Dictionary<int, GameObject>(); // REFACTORIZAR: static Dictionary<int, List<Pool>> objsPool = new Dictionary<int, List<Pool>>();

    Transform playerPos;

    int yDir;
    int xDir;

    public int maxEnemies = 50;
    public int currentEnemies;

    public float waveSpawn = 10;
    public float waveTimer;

    private void Start()
    {
        waveTimer = .5f;

        playerPos = GameObject.Find("Player").transform;

        foreach (Pool pool in pools)
        {
            int id = pool.prefab.GetInstanceID();

            ObjectPooling.PreLoad(pool.tag, pool.prefab, pool.amount);

            objsPool.Add(id, pool.prefab);

            for (int i = 0; i < pool.amount; i++)
            {
                GameObject go = ObjectPooling.GetObject(objsPool[id]);
                go.SetActive(false);
                enemyPool.Add(go);
            }
        }
    }

    private void Update()
    {
        if (waveTimer > 0f)
        {
            waveTimer -= Time.deltaTime;
        }
        else if (waveTimer < 0f)
        {
            waveTimer = 0f;
        }

        Spawn();
    }



    public void Spawn()
    {
        while (currentEnemies < maxEnemies && waveTimer <= 0f)
        {
            int randomX = Random.Range(0, 2);

            if (randomX == 0)
            {
                yDir = Random.Range(0, 2) == 0 ? -6 : 6;
                xDir = Random.Range(-11, 11);
            }
            else if (randomX == 1)
            {
                xDir = Random.Range(0, 2) == 0 ? -11 : 11;
                yDir = Random.Range(-6, 6);
            }

            if (currentEnemies < enemyPool.Count)
            {
                if (!enemyPool[currentEnemies].activeInHierarchy)
                {
                    enemyPool[currentEnemies].transform.position = new Vector2(playerPos.position.x + xDir, playerPos.position.y + yDir);

                    enemyPool[currentEnemies].SetActive(true);
                }
            }
            else if (currentEnemies >= enemyPool.Count)
            {
                foreach (Pool pool in pools)
                {
                    int id = pool.prefab.GetInstanceID();

                    GameObject go = ObjectPooling.GetObject(objsPool[id]);

                    go.name = pool.prefab.name + " " + currentEnemies;

                    go.transform.position = new Vector2(playerPos.position.x + xDir, playerPos.position.y + yDir);

                    enemyPool.Add(go);
                }
            }
            currentEnemies++;
            waveTimer = waveSpawn;
        }
    }

    void Despawn(GameObject containter, GameObject go)
    {
        ObjectPooling.RecicleObject(containter, go);
    }
}