using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomMapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles;
    public int mapWidth = 10;
    public int mapHeight = 10;
    public int mapsCount = 1;
    public Vector2 mapsOffset = new Vector2(10, 10);
    public Transform player;
    public float activationDistance = 10f;


    private void Start()
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        GenerateMultipleMap(playerPosition);
    }

    private void GenerateMultipleMap(Vector3 playerPosition)
    {
        for (int i = 0; i < mapsCount; i++)
        {
            Vector3Int mapPosition = new Vector3Int(
                (int)playerPosition.x - mapWidth / 2,
                (int)playerPosition.y - mapHeight / 2,
                0
            );
            GenerateSingleMap(mapPosition + new Vector3Int((int)mapsOffset.x, (int)mapsOffset.y, 0));
        }
    }


    private void GenerateSingleMap(Vector3Int mapPosition)
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0) + mapPosition;
                int randomTileIndex = Random.Range(0, tiles.Length);
                tilemap.SetTile(cellPosition, tiles[randomTileIndex]);
            }
        }
    }
}
