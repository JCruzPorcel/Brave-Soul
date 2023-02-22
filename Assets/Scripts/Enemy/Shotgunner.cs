using System.Collections;
using UnityEngine;

public class Shotgunner : Enemy
{
    public float normalSpeed;


    float currentRange;
    public float distanceToKeep = 2f;

    float shotTimer = 0f;
    float meleeTimer = 0f;

    // Attack settings

    [Header("Shot")]
    public float shotgunDamage;
    public float shotRange;
    public float waitBetweenShots;
    [SerializeField] Transform shotgunPoint;
    [SerializeField] float shotgunPointRange;

    [Space(5)]

    [Header("Melee")]
    public float meleeDamage;
    public float meleeRange;
    [SerializeField] Transform meleePoint;
    [SerializeField] float meleePointRange;
    public float waitBetweenAttacks;

    bool meleeAttack;
    bool shotAttack;

    [SerializeField] LayerMask playerMask;

    private Vector3 previousPosition = Vector3.zero;

    public override void Spawn()
    {
        normalSpeed = speed;
    }

    public override void Attack()
    {
        currentRange = Vector2.Distance(player.position, transform.position);

        if (shotTimer > 0f)
        {
            animator.SetBool("Shoot", false);
            shotAttack = false;

            shotTimer -= Time.fixedDeltaTime;
        }

        if (meleeTimer > 0f)
        {
            animator.SetBool("Melee Attack", false);
            meleeAttack = false;

            meleeTimer -= Time.fixedDeltaTime;
        }

        if (currentRange < meleeRange)
        {
            if (meleeTimer <= 0)
            {
                MeleeAttack();
            }
        }
        else if (currentRange <= shotRange)
        {
            if (shotTimer <= 0)
            {
                Shot();
            }
        }
    }

    void Shot()
    {
        Vector2 rectSize = new Vector3(shotgunPointRange * 3f, shotgunPointRange);

        Collider2D playerCollider = Physics2D.OverlapBox(shotgunPoint.transform.position, rectSize, 0, playerMask);
        if (playerCollider != null && playerCollider.CompareTag("Character"))
        {
            playerCollider.gameObject.GetComponentInParent<PlayerController>().TakeDamage(shotgunDamage);
        }


        animator.SetBool("Shoot", true);
        shotAttack = true;

        sourceManager.Play("Shotgun SFX");

        shotTimer = waitBetweenShots;
    }

    void MeleeAttack()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(meleePoint.transform.position, meleePointRange, playerMask);
        if (playerCollider != null && playerCollider.CompareTag("Character"))
        {
            playerCollider.gameObject.GetComponentInParent<PlayerController>().TakeDamage(meleeDamage);
        }

        animator.SetBool("Melee Attack", true);
        meleeAttack = true;

        sourceManager.Play("Melee Attack SFX");

        meleeTimer = waitBetweenAttacks;
    }

    public override void Movement()
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
/*
        bool isBelowPlayerY = transform.position.y < player.position.y + 0.03f;
        bool isAbovePlayerY = transform.position.y > player.position.y - 0.03f;
        bool isLeftOfPlayerX = transform.position.x < player.position.x + 0.03f;
        bool isRightOfPlayerX = transform.position.x > player.position.x - 0.03f;

        switch ((isBelowPlayerY, isAbovePlayerY, isLeftOfPlayerX, isRightOfPlayerX))
        {
            case (true, false, _, _):
                sr.sortingOrder = 1;
                break;
            case (false, true, _, _):
                sr.sortingOrder = -1;
                break;
            case (_, _, true, false):
                sr.flipX = false;
                break;
            case (_, _, false, true):
                sr.flipX = true;
                break;
            default:
                break;
        }*/


        // Si se está atacando, detener el movimiento
        if (meleeAttack || shotAttack)
        {
            return;
        }

        float step = speed * Time.fixedDeltaTime;
        float currentDistance = player.position.x - transform.position.x;

        // Animar la velocidad
        float dir = (transform.position - previousPosition).magnitude / Time.fixedDeltaTime;
        animator.SetFloat("Speed", dir);
        previousPosition = transform.position;

        // Mover hacia la posición en el eje Y del jugador
        transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, player.position.y, step), transform.position.z);

        // Si la distancia actual es menor que la distancia a mantener, alejarse
        if (Mathf.Abs(currentDistance) < distanceToKeep)
        {
            Vector3 newPosition = transform.position;
            newPosition.x -= Mathf.Sign(currentDistance) * step;
            transform.position = newPosition;
        }
        // Si la distancia actual es mayor que el rango, seguir el movimiento base
        else if (currentRange > shotRange)
        {
            Vector3 direction = player.position - transform.position;

            direction.Normalize();

            transform.position += direction * step;
        }
    }

    private void OnDrawGizmos()
    {
        if (shotgunPoint == null) return; // Salir si el objeto no está asignado

        // Establecer el color del Gizmo a rojoVector2 rectSize = new Vector3(shotgunPointRange * 3f, shotgunPointRange);
        Gizmos.color = Color.red;

        // Calcular el tamaño del rectángulo
        Vector2 rectSize = new Vector3(shotgunPointRange * 3f, shotgunPointRange);

        // Dibujar el rectángulo en la posición del objeto shotgunPoint
        Gizmos.DrawWireCube(shotgunPoint.position, rectSize);


        if (shotgunPoint == null) return; // Salir si el objeto no está asignado

        // Establecer el color del Gizmo a azul
        Gizmos.color = Color.blue;

        // Dibujar una esfera en la posición del objeto shotgunPoint con el radio especificado por shotgunPointRange
        Gizmos.DrawWireSphere(meleePoint.position, meleePointRange);
    }
}
