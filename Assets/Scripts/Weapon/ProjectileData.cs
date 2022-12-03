using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Create New Projectile")]
public class ProjectileData : ScriptableObject
{
    [SerializeField] private string varName;
    [Header("Stats")]
    [SerializeField] private string itemName;
    [TextArea(5, 5)]
    [SerializeField] private string description;
    [SerializeField] private string upgradeDescription;
    [SerializeField] private int armorPen;
    [SerializeField] private int level;
    [SerializeField] private int projectileAmount;
    [SerializeField] private float speed;
    [SerializeField] private bool canUpgrade;
    [Space(10)]
    [SerializeField] private GameObject prefab;
    [SerializeField] private WeaponData weaponType;
    [SerializeField] private Sprite image;
    [SerializeField] private Sprite weaponUpgradeImage;


    public string VarName { get { return varName; } }

    public int ProjectileAmount { get { return projectileAmount; } }
    public int Level { get { return level; } }
    public int ArmorPen {  get { return armorPen; } }

    public float Speed { get { return speed; } }

    public bool CanUpgrade { get { return canUpgrade; } }

    public Sprite Image { get { return image; } }
    public Sprite WeaponUpgradeImage { get { return WeaponUpgradeImage; } }

    public GameObject Prefab { get { return prefab; } }

    public WeaponData WeaponType { get { return weaponType; } }


    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => description; set => description = value; }
    public string UpgradeDescription { get => upgradeDescription; set => upgradeDescription = value; }

}
