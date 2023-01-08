using UnityEngine;


public class Necronomicon : Weapon
{

    float currentTime;
    int currentAmount;

    [SerializeField] float speedRotation = 40;
    //[SerializeField] int maxAmount;
    [SerializeField] GameObject book2;
    [SerializeField] GameObject go;


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
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            Attack();
            WeaponLevel();
        }
    }

    public override void Attack()
    {
        go.transform.Rotate(0, 0, -speedRotation * Time.deltaTime);

        go.transform.RotateAround(transform.parent.position, new Vector3(0, 0, -1), -speedRotation * Time.deltaTime);


        transform.Rotate(0, 0, speedRotation * Time.deltaTime);

        transform.RotateAround(transform.parent.position, new Vector3(0, 0, -1), speedRotation * Time.deltaTime);
    }

    private void OnDisable()
    {
        WeaponLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);            
        }
    }

    public void WeaponLevel()
    {
        level = GetStatsManager.Instance.level_Necronomicon;

        switch (level)
        {
            case 0:
                GetStatsManager.Instance.level_Necronomicon = 1;
                break;


            case 1:
                damage = 10;
                attackSpeed = 1.25f;
                //maxAmount = 3;
                speedRotation = 40;
                break;

            case 2:
                damage = 15;
                attackSpeed = 1f;
                //maxAmount = 4;
                speedRotation = 95;
                break;

            case 3:
                damage = 20;
                attackSpeed = .75f;
                //maxAmount = 5;
                speedRotation = 140;
                break;

            case 4:
                damage = 20;
                attackSpeed = .50f;
                //maxAmount = 6;
                speedRotation = 180;
                break;

            case 5:
                damage = 25;
                attackSpeed = .35f;
                //maxAmount = 7;
                speedRotation = 250;
                break;

            default:
                Debug.LogWarning("max lvl");
                break;
                
        }
    }
}
