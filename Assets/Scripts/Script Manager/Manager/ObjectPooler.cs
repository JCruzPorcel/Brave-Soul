using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemies
{
    public string name;
    public GameObject prefab;
    public int amount;
}

public class ObjectPooler : MonoBehaviour
{
    [Header("Enemies List")]
    public List<Enemies> enemyPool = new List<Enemies>();

    [Space(5)]
    public List<GameObject> Assassin = new List<GameObject>();
    public List<GameObject> Tank = new List<GameObject>();
    public List<GameObject> Mage = new List<GameObject>();
    public List<GameObject> Boss = new List<GameObject>();

    [Space(15)]
    [Header("Max Enemies")]
    int maxAssassins;
    int maxTanks;
    int maxMages;
    int maxBoss;

    [Space(15)]
    [Header("Current Enemies")]
    int currentAssassins = 0;
    int currentTanks = 0;
    int currentMages = 0;
    int currentBoss = 0;

    [SerializeField]Transform player;

    private void Start()
    {
        foreach (Enemies enemy in enemyPool)
        {
            for (int i = 0; i < enemy.amount; i++)
            {
                GameObject go = Instantiate(enemy.prefab);
                go.SetActive(false);
                if (GameObject.Find(enemy.name + " Pool") != null)
                {
                    go.transform.SetParent(GameObject.Find(enemy.name + " Pool").transform);
                }
                else
                {
                    GameObject parent = new GameObject();
                    parent.name = enemy.name + " Pool";
                    go.transform.SetParent(GameObject.Find(enemy.name + " Pool").transform);
                }

                switch (enemy.name)
                {
                    case "Assassin":
                        Assassin.Add(go);
                        break;
                    case "Tank":
                        Tank.Add(go);
                        break;
                    case "Mage":
                        Mage.Add(go);
                        break;
                    case "Boss":
                        Boss.Add(go);
                        break;
                }
            }
        }
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame) return;

        CheckAndSpawnEnemies();
    }

    void CheckAndSpawnEnemies()
    {
        int minutes = TimerScript.Instance.minutes;
        int seconds = TimerScript.Instance.seconds;

        switch (minutes)
        {
            case 0:
                if (seconds >= 3)
                {
                    maxAssassins = 10;
                    maxMages = 0;
                    maxTanks = 0;
                    maxBoss = 0;
                }
                break;
            case 1:
                if (seconds >= 0)
                {
                    maxAssassins = 15;
                    maxMages = 1;
                    maxTanks = 0;
                    maxBoss = 0;
                }
                break;
            case 3:
                if (seconds == 0)
                {
                    maxAssassins = 20;
                    maxMages = 1;
                    maxTanks = 1;
                    maxBoss = 1;
                }
                break;
            case 5:
                if (seconds == 0)
                {
                    maxAssassins = 30;
                    maxMages = 10;
                    maxTanks = 12;
                    maxBoss = 1;
                }
                break;
            case 7:
                if (seconds >= 0)
                {
                    maxAssassins = 35;
                    maxMages = 12;
                    maxTanks = 10;
                    maxBoss = 0;
                }
                break;
            case 8:
                if (seconds == 0)
                {
                    maxAssassins = 40;
                    maxMages = 15;
                    maxTanks = 20;
                    maxBoss = 2;
                }
                break;
            case 9:
                if (seconds == 0)
                {
                    maxAssassins = 60;
                    maxMages = 45;
                    maxTanks = 35;
                    maxBoss = 3;
                }
                break;
        }
        SpawnAssassins();
        SpawnTanks();
        SpawnMages();
        SpawnBoss();
    }

    void SpawnAssassins()
    {
        if (currentAssassins < maxAssassins)
        {
            for (int i = 0; i < Assassin.Count; i++)
            {
                if (!Assassin[i].activeInHierarchy)
                {
                    Assassin[i].SetActive(true);
                    Vector2 randomPosition = Random.Range(0, 2) == 0 ?
                new Vector2(Random.Range(-11, 11), Random.Range(0, 2) == 0 ? -6 : 6) :
                new Vector2(Random.Range(0, 2) == 0 ? -11 : 11, Random.Range(-6, 6));

                    Assassin[i].transform.position = new Vector3(randomPosition.x + player.position.x, randomPosition.y + player.position.y, 0);

                    currentAssassins++;
                    break;
                }
            }
        }
    }

    void SpawnTanks()
    {
        if (currentTanks < maxTanks)
        {
            for (int i = 0; i < Tank.Count; i++)
            {
                if (!Tank[i].activeInHierarchy)
                {
                    Tank[i].SetActive(true);

                    Vector2 randomPosition = Random.Range(0, 2) == 0 ?
                new Vector2(Random.Range(-11, 11), Random.Range(0, 2) == 0 ? -6 : 6) :
                new Vector2(Random.Range(0, 2) == 0 ? -11 : 11, Random.Range(-6, 6));

                    Tank[i].transform.position = new Vector3(randomPosition.x + player.position.x, randomPosition.y + player.position.y, 0);

                    currentTanks++;
                    break;
                }
            }
        }
    }

    void SpawnMages()
    {
        if (currentMages < maxMages)
        {
            for (int i = 0; i < Mage.Count; i++)
            {
                if (!Mage[i].activeInHierarchy)
                {
                    Mage[i].SetActive(true);

                    Vector2 randomPosition = Random.Range(0, 2) == 0 ?
                new Vector2(Random.Range(-11, 11), Random.Range(0, 2) == 0 ? -6 : 6) :
                new Vector2(Random.Range(0, 2) == 0 ? -11 : 11, Random.Range(-6, 6));

                    Mage[i].transform.position = new Vector3(randomPosition.x + player.position.x, randomPosition.y + player.position.y, 0);

                    currentMages++;
                    break;
                }
            }
        }
    }

    void SpawnBoss()
    {
        if (currentBoss < maxBoss)
        {
            for (int i = 0; i < Boss.Count; i++)
            {
                if (!Boss[i].activeInHierarchy)
                {
                    Boss[i].SetActive(true);

                    Vector2 randomPosition = Random.Range(0, 2) == 0 ?
                new Vector2(Random.Range(-11, 11), Random.Range(0, 2) == 0 ? -6 : 6) :
                new Vector2(Random.Range(0, 2) == 0 ? -11 : 11, Random.Range(-6, 6));

                    Boss[i].transform.position = new Vector3(randomPosition.x + player.position.x, randomPosition.y + player.position.y, 0);

                    currentBoss++;
                    break;
                }
            }
        }
    }
}