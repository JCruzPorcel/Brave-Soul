using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create New Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string varName;
    [Header("Stats")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite image;
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;
    [TextArea(5, 5)]
    [SerializeField] private string description;
    [Space(10)]
    [SerializeField] private int amount;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject projectile;
    [Header("Weapon Evolve")]
    [SerializeField] private bool canEvolve;
    [SerializeField] private string evName;
    [SerializeField] private Sprite evImage;
    [TextArea(5, 5)]
    [SerializeField] private string evDescription;
    [SerializeField] private int level;
    [SerializeField] private int weaponPen;
    [SerializeField] private int amountWeapon;

    public string VarName { get { return varName; } }
    public string ItemName { get { return itemName; } }
    public int Damage { get { return damage; } }
    public float AttackSpeed { get { return attackSpeed; } }
    public Sprite Image { get { return image; } }
    public string Description { get { return description; } }
    public GameObject Prefab { get { return prefab; } }
    public int Amount { get { return amount; } }
    public bool CanEvolve { get { return canEvolve; } }
    public Sprite EvImage { get { return evImage; } }
    public string EvDescription { get { return evDescription; } }
    public int Level { get { return level; } }
    public int WeaponPen { get { return weaponPen; } }
    public int AmountWeapon { get { return amountWeapon; } }
    public GameObject Projectile { get { return projectile; } }
}
