using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float playerSpeed;
    public int maxHealth;
    [Min(0)] public float currentHealth;
    public int heal;
    [Min(0)] public int currentLvl = 0;
    public int maxLevel = 100;
    public int currentExp = 0;
    public int nextLvl = 10;
    public int pointsLvl;

    [SerializeField] public bool godMode;
    bool isDead = false;
    bool _facingRight;

    //Reference's
    Animator anim;
    SpriteRenderer sr;
    [SerializeField] SliderBar sliderBar;

    public bool FacingRight { get { return _facingRight; } }
    public bool IsDead { get { return isDead; } }
    public bool GodMode { get => godMode; set => godMode = value; }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.currentGameState != GameState.inGame) return;

        GodMode = GameManager.Instance.GodMode;

        if (!isDead)
        {
            Movement();
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }

    void Movement()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        Vector3 direcction = new Vector3(xMove, yMove, 0f) * playerSpeed * Time.deltaTime;

        transform.position += direcction;


        if (xMove > .01f)
        {
            _facingRight = true;
        }
        else if (xMove < -.01f)
        {
            _facingRight = false;
        }

        if (direcction != Vector3.zero)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        if (_facingRight)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;


        currentHealth -= damage;

        sliderBar.SetHealth(currentHealth);

        anim.SetTrigger("Hit");

        if (godMode)
            return;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            anim.SetBool("IsDead?", isDead);
        }
    }

    public void TakeHealth(float heal)
    {
        currentHealth += heal;

        sliderBar.SetHealth(currentHealth);

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeExp(int exp)
    {
        currentExp += exp;

        if (currentExp >= nextLvl)
        {
            pointsLvl++;

            if (pointsLvl == 1)
            {
                UIManager.Instance.LevelUp();
            }

            currentLvl++;
            nextLvl *= 2;
        }
    }    
}
