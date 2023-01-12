using UnityEngine;

public class ExperienceOrbPooling : Singleton<ExperienceOrbPooling>
{
    public GameObject experienceOrbPrefab;
    public int poolSize = 5;
    public Transform player;

    private GameObject[] pooledObjects;
    private int currentIndex = 0;

    public int minOrbExp;
    public int maxOrbExp;

    public float spawnRadius = 2f;
    public float despawnRadius = 10f;
    private Camera cam;

    public GameObject expContainer;

    void Start()
    {
        cam = Camera.main;

        pooledObjects = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(experienceOrbPrefab, expContainer.transform);
            obj.transform.SetParent(expContainer.transform);
            obj.SetActive(false);
            pooledObjects[i] = obj;
        }
    }

    void Update()
    {
        for (int i = 0; i < pooledObjects.Length; i++)
        {
            GameObject obj = pooledObjects[i];
            if (obj.activeInHierarchy)
            {
                if (Vector3.Distance(obj.transform.position, player.position) > despawnRadius)
                {
                    obj.SetActive(false);
                }
            }
            else
            {
                if (IsInCameraView(obj.transform))
                {
                    obj.SetActive(true);
                }
            }
        }
    }

    public void SpawnExperienceOrb(Vector3 position)
    {
        GameObject obj = pooledObjects[currentIndex];
        obj.transform.position = position;
        obj.GetComponent<ExperienceOrb>().SetOrbExp(minOrbExp, maxOrbExp);
        obj.SetActive(true);
        currentIndex = (currentIndex + 1) % poolSize;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        minOrbExp = enemy.min_exp;
        maxOrbExp = enemy.max_exp;
        SpawnExperienceOrb(enemy.transform.position);
    }

    private bool IsInCameraView(Transform target)
    {
        Vector3 pointOnScreen = cam.WorldToViewportPoint(target.position);
        return (pointOnScreen.x > 0 && pointOnScreen.x < 1 && pointOnScreen.y > 0 && pointOnScreen.y < 1);
    }

}
