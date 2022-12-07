using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Create New Character")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string varName;
    [SerializeField] private string charName;
    [TextArea(5,6)]
    [SerializeField] private string description;
    [SerializeField] private int armor;
    [SerializeField] private int damage;
    [SerializeField] private int maxHp;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float hpRegen;
    [SerializeField] private float speed;
    [SerializeField] private int charPrice;
    [SerializeField] private Sprite charImage;
    [SerializeField] private WeaponData startWeapon;
    [SerializeField] private GameObject charPrefab;

    [SerializeField] private bool isOwned = false;
    [SerializeField] private bool itsBuyable = true;


    public string VarName { get { return varName; } }

    public string CharName { get => charName; set => charName = value; }
    public string Description { get => description; set => description = value; }
    public int Armor { get => armor; set => armor = value; }
    public int Damage { get => damage; set => damage = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int CharPrice { get => charPrice; set => charPrice = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float HpRegen { get => hpRegen; set => hpRegen = value; }
    public float Speed { get => speed; set => speed = value; }

    public bool IsOwned { get => isOwned; set => isOwned = value; }
    public bool ItsBuyable { get => itsBuyable; set => itsBuyable = value; }

    public Sprite CharImage { get { return charImage; } }
    public WeaponData StartWeapon { get { return startWeapon; } }
    public GameObject CharPrefab { get { return charPrefab; } }

}
