using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorGenerator : MonoBehaviour
{
    public GameObject[] floorTiles; 
    public float tileSize; 
    public int viewRange; 
    public float spawnDistance; 

    private Transform player;
    private Vector3 spawnPosition;
    private List<GameObject> spawnedTiles = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPosition = player.position;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, spawnPosition) > spawnDistance)
        {
            int randomTile = Random.Range(0, floorTiles.Length);
            GameObject newTile = Instantiate(floorTiles[randomTile], spawnPosition, Quaternion.identity);
            spawnedTiles.Add(newTile);
            spawnPosition = player.position;

            CheckDeactivation();
        }
    }

    void CheckDeactivation()
    {
        for (int i = 0; i < spawnedTiles.Count; i++)
        {
            float distance = Vector3.Distance(player.position, spawnedTiles[i].transform.position);
            if (distance > viewRange)
            {
                spawnedTiles[i].SetActive(false);
            }
            else
            {
                spawnedTiles[i].SetActive(true);
            }
        }
    }
}
