using UnityEngine;

public class FloatingSprite : MonoBehaviour
{
    public GameObject spritePrefab;
    public float movementSpeed = 0.1f;
    public float fadeSpeed = 0.1f;

    public Sprite gameOverSprite;
    public Sprite levelUpSprite;
    public Sprite forgeWeaponSprite;

    public PlayerController playerController;

    [SerializeField] Transform player;
    private GameObject[] pooledObjects;
    private int currentIndex = 0;
    private int poolSize = 5;

    public GameObject spriteContainer;

    void Start()
    {
        //player = GameObject.FindWithTag("Player").transform;
        pooledObjects = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(spritePrefab);
            obj.SetActive(false);
            pooledObjects[i] = obj;
            obj.transform.SetParent(spriteContainer.transform);
        }
    }

    public void SpawnSpriteLevelUp()
    {
        GameObject obj = pooledObjects[currentIndex];
        obj.transform.position = player.position;
        obj.GetComponent<SpriteRenderer>().sprite = levelUpSprite;
        obj.SetActive(true);
        currentIndex = (currentIndex + 1) % poolSize;
    }


    public void SpawnSpriteGameOver()
    {
        GameObject obj = pooledObjects[currentIndex];
        obj.transform.position = player.position;
        obj.GetComponent<SpriteRenderer>().sprite = gameOverSprite;
        obj.SetActive(true);
        currentIndex = (currentIndex + 1) % poolSize;
    }


    public void SpawnSpriteForge()
    {
        GameObject obj = pooledObjects[currentIndex];
        obj.transform.position = player.position;
        obj.GetComponent<SpriteRenderer>().sprite = forgeWeaponSprite;
        obj.SetActive(true);
        currentIndex = (currentIndex + 1) % poolSize;
    }


    void Update()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = pooledObjects[i];
            if (obj.activeInHierarchy)
            {
                obj.transform.position += Vector3.up * movementSpeed * Time.deltaTime;

                var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                var color = spriteRenderer.color;
                color.a -= fadeSpeed * Time.deltaTime;
                spriteRenderer.color = color;

                if (color.a <= 0)
                {
                    obj.SetActive(false);
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                    obj.transform.position = player.position;
                }
            }
        }
    }

}
