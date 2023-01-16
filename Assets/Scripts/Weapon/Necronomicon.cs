using UnityEngine;


public class Necronomicon : Weapon
{
    [SerializeField] float speedRotation = 40;
    [SerializeField] GameObject book2;
    [SerializeField] GameObject go;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite evSprite;
    public string normalName;
    public string evolutionName;

    private void Start()
    {
        WeaponLevel();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        go = Instantiate(book2, player);

        go.transform.position = new Vector2(player.position.x, player.position.y - 1);
        go.transform.SetParent(player);

        transform.position = new Vector2(player.position.x, player.position.y + 1);
        transform.SetParent(player);
    }

    private void Update()
    {
        WeaponLevel();
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        go.transform.Rotate(0, 0, -speedRotation * Time.deltaTime);

        go.transform.RotateAround(transform.parent.position, new Vector3(0, 0, -1), -speedRotation * Time.deltaTime);


        transform.Rotate(0, 0, speedRotation * Time.deltaTime);

        transform.RotateAround(transform.parent.position, new Vector3(0, 0, -1), speedRotation * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public override void WeaponLevel()
    {
        level = LevelUpManager.Instance.level_Necronomicon;

        texts = LanguageManager.Instance.texts;

        weaponData.ItemName = texts[weaponData.ID];
        normalName = texts[weaponData.ID];
        evolutionName = texts[weaponData.ID + 7];

        if (level <= -1)
        {
            weaponData.Description = texts[weaponData.ID + 1];
        }
        else if (level <= 5)
        {
            weaponData.Description = texts[weaponData.ID + level + 2];
        }

        switch (level)
        {
            case -1:
                damage = 10;
                speedRotation = 40;
                weaponData.WeaponImage = normalSprite;
                weaponData.ItemName = normalName;
                break;

            case 0:
                damage = 10;
                speedRotation = 40;
                weaponData.WeaponImage = normalSprite;
                weaponData.ItemName = normalName;
                break;

            case 1:
                damage = 12;
                speedRotation = 60;
                weaponData.WeaponImage = normalSprite;
                weaponData.ItemName = normalName;
                break;

            case 2:
                damage = 15;
                speedRotation = 95;
                weaponData.WeaponImage = normalSprite;
                weaponData.ItemName = normalName;
                break;

            case 3:
                damage = 20;
                speedRotation = 140;
                weaponData.WeaponImage = evSprite;
                weaponData.ItemName = evolutionName;
                break;

            case 4:
                damage = 20;
                speedRotation = 180;
                weaponData.WeaponImage = evSprite;
                weaponData.ItemName = evolutionName;
                break;

            case 5:
                damage = 25;
                speedRotation = 250;
                weaponData.WeaponImage = evSprite;
                weaponData.ItemName = evolutionName;
                break;
        }
    }
}
