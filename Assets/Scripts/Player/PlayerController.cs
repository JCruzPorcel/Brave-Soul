using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float playerSpeed;
    public int maxHealth;
    [Min(0)] public float currentHealth;
    public float heal;
    [Min(0)] public int currentLvl = 0;
    public int nextLvl = 10;
    public int currentExp = 0;
    public int pointsLvl;

    [SerializeField] public bool godMode;
    bool isDead = false;
    //bool _facingRight;

    //Reference's
    Animator anim;
    SpriteRenderer sr;
    [SerializeField] SliderBar sliderBar;

    //public bool FacingRight { get { return _facingRight; } }
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool GodMode { get => godMode; set => godMode = value; }
    bool dead = false;

    [SerializeField] FloatingSprite floatingSprite;
    [SerializeField] MusicFader musicFader;

    AudioManager audioManager;
    private bool isMoving = false;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Character").GetComponent<Animator>();
        sr = GameObject.FindGameObjectWithTag("Character").GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();

        heal = GameManager.Instance.CharSelected.HpRegen;
        maxHealth = GameManager.Instance.CharSelected.MaxHp;
        playerSpeed = GameManager.Instance.CharSelected.Speed;
        currentHealth = maxHealth;
        sliderBar.SetMaxtHealth(maxHealth);
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame) { audioManager.Pause("FootStep SFX"); return; };

        PlayerNextlevel();

        audioManager.UnPause("FootStep SFX");

        TakeHealth(heal);
    }

    private void FixedUpdate()
    {
        //GodMode = GameManager.Instance.GodMode;

        if (GameManager.Instance.currentGameState != GameState.inGame)
        {
            if (GameManager.Instance.currentGameState != GameState.gameOver)
            {
                anim.speed = 0;
            }

            return;
        }

        anim.speed = 1;

        if (!dead)
        {
            Movement();
        }
    }

    void Movement()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(xMove, yMove, 0f) * playerSpeed * Time.deltaTime;

        transform.position += direction;


        if (xMove > .01f)
        {
            //_facingRight = true;
            sr.flipX = false;
        }
        else if (xMove < -.01f)
        {
            //_facingRight = false;
            sr.flipX = true;
        }

        anim.SetFloat("Speed", direction.magnitude);


        if (direction.magnitude != 0f)
        {
            if (!isMoving)
            {
                audioManager.Play("FootStep SFX");
                isMoving = true;
            }
        }
        else
        {
            audioManager.Stop("FootStep SFX");
            isMoving = false;
        }



        /*
                if (_facingRight)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }*/
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        sliderBar.SetHealth(currentHealth);

        anim.SetTrigger("Hit");

        if (godMode)
            return;

        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            musicFader.FadeOut(.5f);
            GameManager.Instance.GameOver();
            audioManager.Play("GameOver SFX");
            anim.SetBool("IsDead?", true);
            floatingSprite.SpawnSpriteGameOver();
            currentHealth = 0;
        }
    }

    public void TakeHealth(float heal)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += heal * Time.deltaTime;

            sliderBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeExp(int exp)
    {
        currentExp += exp;   
    }

    void PlayerNextlevel()
    {
        if (currentExp >= nextLvl)
        {

            if (!LevelUpManager.Instance.maxLevel)
            {
                audioManager.Play("LevelUp SFX");
                floatingSprite.SpawnSpriteLevelUp();
                pointsLvl++;
                MenuManager.Instance.LevelUp();
            }
            currentExp -= nextLvl;
            currentLvl++;

            if (nextLvl < 800)
            {
                nextLvl *= 2;
            }
        }
    }
}
