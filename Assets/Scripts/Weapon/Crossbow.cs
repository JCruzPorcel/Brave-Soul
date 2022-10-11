using UnityEngine;

public class Crossbow : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    Transform player;
    Transform container;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        container = GameObject.Find("Container").transform;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Arrow();
        }
    }

    void Arrow()
    {
        GameObject go = Instantiate(weaponData.Projectile, player, container);
    }

    
}
