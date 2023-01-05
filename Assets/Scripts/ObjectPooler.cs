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
    [SerializeField] int maxAssassins;
    [SerializeField] int maxTanks;
    [SerializeField] int maxMages;


    [Space(15)]
    [Header("Current Enemies")]
    [SerializeField] int currentAssassins = 0;
    [SerializeField] int currentTanks = 0;
    [SerializeField] int currentMages = 0;
    [SerializeField] int currentBosses = 0;

    bool canSpawnBosses;

    Transform player;


    [Space(15)]
    [Header("Timer To Spawn")]
    [SerializeField] float timeToSpawn;
    float currentTime;


    private void Start()
    {
        foreach (Enemies enemy in enemyPool)
        {
            for (int i = 0; i < enemy.amount; i++)
            {

                GameObject go = Instantiate(enemy.prefab);

                if (enemy.name == "Assassin")
                {
                    Assassin.Add(go);
                }
                else if (enemy.name == "Tank")
                {
                    Tank.Add(go);
                }
                else if (enemy.name == "Mage")
                {
                    Mage.Add(go);
                }
                else if (enemy.name == "Boss")
                {
                    Boss.Add(go);
                }

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
            }
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame) return;


        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }

        Spawn();
    }



    int yDir;
    int xDir;

    void Spawn()
    {

        if (currentTime <= 0)
        {
            int randomX = Random.Range(0, 2);
            if (TimerScript.Instance.minutes == 0 && TimerScript.Instance.seconds == 3)
            {
                maxAssassins = 5;
                maxMages = 0;
                maxTanks = 0;
            }            
            else if (TimerScript.Instance.minutes == 3 && TimerScript.Instance.seconds == 0)
            {
                maxAssassins = 8;
                maxMages = 0;
                maxTanks = 1;
            }
            else if (TimerScript.Instance.minutes == 5 && TimerScript.Instance.seconds == 0)
            {
                canSpawnBosses = true;

                maxAssassins = 10;
                maxMages = 1;
                maxTanks = 2;
            }
            else if (TimerScript.Instance.minutes == 7 && TimerScript.Instance.seconds == 0)
            {
                canSpawnBosses = true;

                maxAssassins = 20;
                maxMages = 2;
                maxTanks = 3;
            }
            else if (TimerScript.Instance.minutes == 9 && TimerScript.Instance.seconds == 0)
            {
                canSpawnBosses = true;

                maxAssassins = 50;
                maxMages = 25;
                maxTanks = 18;
            }
            else if (TimerScript.Instance.minutes == 10 && TimerScript.Instance.seconds == 0)
            {
                //ToDo: End Game;
            }

            if (currentAssassins < maxAssassins)
            {
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

                Assassin[currentAssassins].transform.position = new Vector2(player.position.x + xDir, player.position.y + yDir);

                Assassin[currentAssassins].SetActive(true);
                currentAssassins++;
            }

            if (currentTanks < maxTanks)
            {
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

                Tank[currentTanks].transform.position = new Vector2(player.position.x + xDir, player.position.y + yDir);

                Tank[currentTanks].SetActive(true);
                currentTanks++;
            }

            if (currentMages < maxMages)
            {
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

                Mage[currentMages].transform.position = new Vector2(player.position.x + xDir, player.position.y + yDir);

                Mage[currentMages].SetActive(true);
                currentMages++;
            }

            if (canSpawnBosses)
            {
                if(currentBosses >= Boss.Count)
                {
                    currentBosses = 0;
                }

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

                Boss[currentBosses].transform.position = new Vector2(player.position.x + xDir, player.position.y + yDir);

                Boss[currentBosses].SetActive(true);
                currentBosses++;

                canSpawnBosses = false;
            }

            currentTime = timeToSpawn;
        }

    }
}
