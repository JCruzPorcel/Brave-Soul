using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Create New Projectile")]
public class ProjectileData : ScriptableObject
{
    [SerializeField] private string varName;
    [Header("Stats")]
    [SerializeField] private string itemName;
    [SerializeField] private float speed;
    [TextArea(5, 5)]
    [SerializeField] private string description;
    [SerializeField] private int armorPen;
    [SerializeField] private int level;
    [SerializeField] private GameObject prefab;
    [SerializeField] private WeaponData weaponType;
    [SerializeField] private int projectileAmount;


    public string VarName { get { return varName; } }
    public string ItemName { get { return itemName; } }
    public float Speed { get { return speed; } }
    public string Description { get { return description; } }
    public int ArmorPen { get { return armorPen; } }
    public int Level { get { return level; } }
    public GameObject Prefab { get { return prefab; } }
    public WeaponData WeaponType { get { return weaponType; } }
    public int ProjectileAmount { get { return projectileAmount; } }

}
