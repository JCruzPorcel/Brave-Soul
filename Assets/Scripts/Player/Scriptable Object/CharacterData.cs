using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Create New Character")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string varName;
    [SerializeField] private string charName;
    [SerializeField] private string description;
    [SerializeField] private int damage;
    [SerializeField] private int s;





    /* DELETE THIS BEFORE USE
    Transform player;
    [SerializeField] WeaponData weaponData;
    [SerializeField] GameObject weaponContainer;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        GameObject go = Instantiate(weaponData.Prefab, weaponContainer.transform);
        go.transform.position = new Vector3(player.position.x, player.position.y + .2f);
    }*/
}
