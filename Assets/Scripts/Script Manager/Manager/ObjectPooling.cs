using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject[] objects;
    public int poolSize = 5;
    public float spawnRadius = 2f;
    public float despawnRadius = 10f;
    public Transform player;

    private GameObject[] pooledObjects;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        pooledObjects = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, objects.Length);
            GameObject obj = Instantiate(objects[randomIndex], RandomSpawnPosition(), Quaternion.identity);
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

    private Vector3 RandomSpawnPosition()
    {
        Vector3 spawnPos = player.position + Random.insideUnitSphere * spawnRadius;
        spawnPos.y = 0f;
        return spawnPos;
    }

    private bool IsInCameraView(Transform target)
    {
        Vector3 pointOnScreen = cam.WorldToViewportPoint(target.position);
        return (pointOnScreen.x > 0 && pointOnScreen.x < 1 && pointOnScreen.y > 0 && pointOnScreen.y < 1);
    }
}
