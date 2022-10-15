using UnityEngine;

public class Crossbow : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    Transform player;
    Transform container;
    GameObject[] enemies;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        container = GameObject.Find("Container").transform;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            GameObject go = Instantiate(weaponData.Projectile, player);
            go.transform.SetParent(container);
        }
    }
}
