using UnityEngine;

public class Boss : Enemy //Refactorizar a boss
{
    public GameObject m_Chest;
    public bool isSpawned = false;

    // Variables
    public float normalSpeed = 5f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float slashSpeed = 10f;
    [SerializeField] private float stopDistance = 1.5f;
    public float CooldownSlash = 1f;
    private bool canUseSlash = true;
    public bool isDashing = false;
    public float currentSlashCD = 0f;
    public Vector2 lastPlayerPos = Vector2.zero;
    private Vector3 previousPosition = Vector3.zero;
    private Vector3 rbPreviousPos;
    public bool canCheckFlipX = true;


    [SerializeField] Transform dashPoint;
    //[SerializeField] Transform meleePoint;

    [SerializeField] float dashPointRange = 1f;

    //[SerializeField] float meleePointRange = 1f;
    [SerializeField] float slashDamage = 1f;
    //[SerializeField] float meleeDamage = 1f;

    [SerializeField] LayerMask playerMask;


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
    }

    public override void Attack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        float step = speed * Time.fixedDeltaTime;

        // Check if the enemy can use slash
        if (!canUseSlash && !isDashing)
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
                isDashing = true;
                canCheckFlipX = false;
                canUseSlash = false;
                animator.SetBool("isDashing", true);
                animator.SetTrigger("Dash");
            }
        }
    }

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

        SlashDamage();
    }

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

        // Comprueba si el personaje se está moviendo hacia abajo
        bool movingDown = lastPlayerPos.y > transform.position.y + 0.5f;

        sr.flipX = false;

        if (lastPlayerPos.x < transform.position.x || (movingDown && lastPlayerPos.x > transform.position.x))
        {
            // Si el jugador se mueve hacia la izquierda o hacia abajo y a la derecha del personaje, voltear en Y
            sr.flipY = true;
        }
        else if (lastPlayerPos.x > transform.position.x)
        {
            // Si el jugador se mueve hacia la derecha, no voltear en Y
            sr.flipY = false;
        }

        // Crea una rotación en el eje Z utilizando Euler
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Aplica la rotación al objeto
        transform.rotation = rotation;
    }

    public void SlashDamage()
    {
        Vector2 rectSize = new Vector3(dashPointRange, dashPointRange);

        Collider2D playerCollider = Physics2D.OverlapBox(dashPoint.position, rectSize, 0, playerMask);

        if (playerCollider != null && playerCollider.CompareTag("Character"))
        {
            playerCollider.gameObject.GetComponentInParent<PlayerController>().TakeDamage(slashDamage);
        }
    }

    private void OnDrawGizmos()
    {
        if (dashPoint == null) return; // Salir si el objeto no está asignado

        // Establecer el color del Gizmo a magenta
        Gizmos.color = Color.magenta;

        // Calcular el tamaño del rectángulo
        Vector2 rectSize = new Vector3(dashPointRange, dashPointRange);

        // Dibujar el rectángulo en la posición del objeto shotgunPoint
        Gizmos.DrawWireCube(dashPoint.position, rectSize);
    }
}
