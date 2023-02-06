using UnityEngine;

public class Shotgunner : Enemy
{
    public float normalSpeed;
    CapsuleCollider2D capsuleCollider2D;
    CircleCollider2D circleCollider2D;
    [Tooltip("Capsule Collider 'Shotgun'")] public float offsetValue;
    [Tooltip("Circle Collider 'Melee'")] public float offsetValue2;

    // Attack settings
    public float range;
    public float meleeRange;

    float currentRange;
    public float distanceToKeep = 2f;

    public float attackTime;
    float timer = 0f;

    public float meleeDamage;
    public float shotgunDamage;
    public float normalDamage;

    bool meleeAttack;
    bool shotAttack;

    public float meleeAttackTimer;
    float meleeTimer = 0f;

    private Vector3 previousPosition = Vector3.zero;
    

    public override void Spawn()
    {
        normalDamage = damage;
        normalSpeed = speed;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();        
    }

    public override void Attack()
    {
        currentRange = Vector2.Distance(player.position, transform.position);

        capsuleCollider2D.offset = sr.flipX ? new Vector2(-offsetValue, capsuleCollider2D.offset.y) : new Vector2(offsetValue, capsuleCollider2D.offset.y);
        circleCollider2D.offset = sr.flipX ? new Vector2(-offsetValue2, circleCollider2D.offset.y) : new Vector2(offsetValue2, circleCollider2D.offset.y);

        if (timer > 0f)
        {
            animator.SetBool("Shoot", false);
            shotAttack = false;

            timer -= Time.fixedDeltaTime;
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
        else if (currentRange <= range)
        {
            if (timer <= 0)
            {
                Shoot();
            }
        }
        else
        {
            damage = normalDamage;
        }
    }

    void Shoot()
    {
        damage = shotgunDamage;
        animator.SetBool("Shoot", true);
        shotAttack = true;

        sourceManager.Play("Shotgun SFX");

        timer = attackTime;
    }

    void MeleeAttack()
    {
        damage = meleeDamage;
        animator.SetBool("Melee Attack", true);
        meleeAttack = true;

        sourceManager.Play("Melee Attack SFX");

        meleeTimer = meleeAttackTimer;
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


        // Si se está atacando, detener el movimiento
        if (meleeAttack || shotAttack)
        {
            speed = 0;
            return;
        }
        else
        {
            speed = normalSpeed;
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
        else if (currentRange > range)
        {
            Vector3 direction = player.position - transform.position;

            direction.Normalize();

            transform.position += direction * step;
        }
    }

}
