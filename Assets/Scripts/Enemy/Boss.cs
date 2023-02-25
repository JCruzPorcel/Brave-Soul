using UnityEngine;

public class Boss : Enemy
{
    public GameObject m_Chest;
    public bool isSpawned;

    // Variables
    public float normalSpeed = 5f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float slashSpeed = 10f;
    [SerializeField] private float stopDistance = 1.5f;
    //[SerializeField] private float dashDuration = 0.3f;
    //[SerializeField] private float secondDashDuration = 0.3f;
    //[SerializeField] private float chargeTimer = 0.5f;
    public float CooldownSlash = 1f;
    private bool canUseSlash = true;
    public bool isDashing = false;
    //private bool firstDash = false;
    //private bool onCharging = false;
    //private bool onSlashing = false;
    public float currentSlashCD = 0f;
    //private float currentDashTime = 0f;
    //private float currentChargeTime = 0f;
    public Vector2 lastPlayerPos = Vector2.zero;
    private Vector3 previousPosition = Vector3.zero;
    private Vector3 rbPreviousPos;
    public bool canCheckFlipX;

    public override void Spawn()
    {
        normalSpeed = speed;
    }

    public void BossDeath()
    {
        if (!isSpawned)
        {
            isSpawned = true;
            GameObject go = Instantiate(m_Chest);
            go.transform.position = this.transform.position;
        }
    }

    public override void Movement()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        float step = speed * Time.fixedDeltaTime;
        float dir = (transform.position - previousPosition).magnitude / Time.fixedDeltaTime;
        previousPosition = transform.position;
        animator.SetFloat("Speed", dir);

        if (canCheckFlipX)
        {
            if (transform.position.y < player.position.y + .03f)
            {
                sr.sortingOrder = 1;
            }
            else if (transform.position.y > player.position.y - .03f)
            {
                sr.sortingOrder = -1;
            }

            if (transform.position.x < player.position.x + .03f)
            {
                sr.flipX = false;
            }
            else if (transform.position.x > player.position.x - .03f)
            {
                sr.flipX = true;
            }
        }

        // Check if the enemy can use slash
        if (!canUseSlash)
        {
            // Decrease current time to use slash
            currentSlashCD -= Time.deltaTime;

            if (currentSlashCD <= 0f)
            {
                canUseSlash = true;
            }
        }

        // Move towards player if not dashing and within stop distance
        if (distanceToPlayer > stopDistance && !isDashing)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            transform.position += direction * step;
        }
        // Use slash if within stop distance and can use slash
        else if (distanceToPlayer <= stopDistance && !isDashing)
        {

            if (canUseSlash)
            {
                // Set up for dash and slash
                lastPlayerPos = transform.position - player.position;
                //  currentDashTime = dashDuration;
                // currentChargeTime = chargeTimer;
                isDashing = true;
                //firstDash = true;
                canCheckFlipX = false;
                canUseSlash = false;
                animator.SetBool("isDashing", true);
                animator.SetTrigger("Dash");
            }
        }

        // Handle dash and slash
        /*if (isDashing)
        {
            //Slash();
        }
        */
    }

    /*
    void Slash()
    {
        if (firstDash)
        {
            // Perform first dash
            currentDashTime -= Time.deltaTime;
            rb.velocity = lastPlayerPos.normalized * dashSpeed;

            if (currentDashTime <= 0f)
            {
                speed = 0;
                rb.velocity = Vector2.zero;
                firstDash = false;
                onCharging = true;
            }
        }
        else if (onCharging)
        {
            // Charge up for second dash
            currentChargeTime -= Time.deltaTime;

            if (currentChargeTime <= 0f)
            {
                lastPlayerPos = player.position - transform.position;
                currentDashTime = secondDashDuration;
                onCharging = false;
                onSlashing = true;
            }
        }
        else if (onSlashing)
        {
            // Perform second dash for slash
            currentDashTime -= Time.deltaTime;
            rb.velocity = lastPlayerPos.normalized * secondDashSpeed;

            if (currentDashTime <= 0f)
            {
                rb.velocity = Vector2.zero;
                onSlashing = false;
            }
        }
        else
        {
            // Reset variables for next slash
            currentTimeSlash = slashTimer;
            speed = normalSpeed;
            canUseSlash = false;
            isDashing = false;
        }
    }
    */

    public void DashAwayFromPlayer()
    {
        // Actualiza el movimiento del Rigidbody
        UpdateRigidbodyMovement(rb, lastPlayerPos, ref rbPreviousPos);

        rb.velocity = (lastPlayerPos.normalized * dashSpeed);
    }

    public void Slash()
    {
        // Actualiza el movimiento del Rigidbody
        UpdateRigidbodyMovement(rb, lastPlayerPos, ref rbPreviousPos);

        rb.velocity = (lastPlayerPos.normalized * slashSpeed);
    }

    /*private bool CanUseSlash()
    {
        // Comprueba si el jugador está en el rango en el eje x y el mismo eje y
        bool isInRangeY = Mathf.Abs(transform.position.y - player.position.y) < .03f
            && Mathf.Abs(transform.position.x - player.position.x) <= stopDistance;

        // Si el jugador está en el rango, comprueba si se puede usar el slash
        if (isInRangeY)
        {
            return true;
        }

        return false;
    }
    */
    public void UpdateRigidbodyMovement(Rigidbody2D rb, Vector3 lastPlayerPos, ref Vector3 previousPlayerPos)
    {
        // Calcula la dirección del movimiento
        Vector3 moveDirection = (lastPlayerPos - previousPlayerPos).normalized;

        // Calcula la velocidad del movimiento
        float moveSpeed = (lastPlayerPos - previousPlayerPos).magnitude / Time.deltaTime;

        // Actualiza la velocidad del Rigidbody
        rb.velocity = moveDirection * moveSpeed;

        // Actualiza la posición anterior del jugador
        previousPlayerPos = lastPlayerPos;
    }

    public void OnDashRotation()
    {
        // Calcula el ángulo de rotación en radianes utilizando Atan2
        float angle = Mathf.Atan2(lastPlayerPos.y, lastPlayerPos.x) * Mathf.Rad2Deg;

        // Ajusta el ángulo de rotación si el personaje está moviéndose hacia abajo
        if (lastPlayerPos.y > transform.position.y + .5f)
        {
            sr.flipY = true;
            sr.flipX = false;
            Debug.Log(string.Format("1°ro: flipY: {0}, flipX: {1}", sr.flipY, sr.flipX));
        }else if(lastPlayerPos.x < transform.position.x)
        {
            sr.flipY = true;
            sr.flipX = false;
            Debug.Log(string.Format("2°to: flipY: {0}, flipX: {1}", sr.flipY, sr.flipX));
        }
        else if (lastPlayerPos.x > transform.position.x)
        {
            sr.flipY = false;
            Debug.Log(string.Format("3°ro: flipY: {0}, flipX: {1}", sr.flipY, sr.flipX));
        }
        else if (lastPlayerPos.x > transform.position.x && lastPlayerPos.y > transform.position.y + .5f)
        {
            sr.flipY = true;
            sr.flipX = false;
            Debug.Log(string.Format("4°to: flipY: {0}, flipX: {1}", sr.flipY, sr.flipX));
        }

        // Crea una rotación en el eje Z utilizando Euler
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Aplica la rotación al objeto
        transform.rotation = rotation;
    }
}
