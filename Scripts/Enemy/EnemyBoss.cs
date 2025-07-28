using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    private Animator animator;
    private Transform player;
    private EnemyMovement movement;
    private EnemyPatrol patrol;

    public Transform gun;

    [Header("Enemy Settings")]
    public float speed = 2f;
    public float chaseRange = 5f;                // khoản cách phát hiện ng chơi
    public float attackRange = 1f;               // khoảng cách tấn công
    public float attackCooldown = 1.5f;

    private enum EnemyState { Idle, Patrolling, Chasing }
    private EnemyState currentState = EnemyState.Patrolling;

    [Header("Gun Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float lastShootTime = -Mathf.Infinity;

    public float preferredDistance = 5f;    // khoảng cách lí tưởng để bắn
    private float retreatStartTime = -1f;
    private bool isRetreating = false;
    public float retreatDuration = 3f;      // thời gian lùi

    [Header("Retreat Settings")]
    public float retreatSpeed = 1f;           // tốc độ lùi

    [Header("Auto Circle Shoot")]
    public float timeBullet = 5f;
    public int bulletCount = 8;        // số viên đạn trong 1 vòng
    private float lastCircleShootTime = -Mathf.Infinity;


    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
        patrol = GetComponent<EnemyPatrol>();

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        currentState = (distance <= chaseRange) ? EnemyState.Chasing : EnemyState.Patrolling;

        HandleState();

        //bắn đạn xung quanh
        if (Time.time - lastCircleShootTime >= timeBullet)
        {
            lastCircleShootTime = Time.time;
            ShootCircle();
        }


    }

    void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.Chasing:
                HandleChase();
                break;
            case EnemyState.Patrolling:
                HandlePatrol();
                break;
            case EnemyState.Idle:
                animator.SetBool("isMoving", false);
                movement.Stop();
                break;
        }
    }

    void HandleChase()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;

        UpdateFacing(direction);

        if (isRetreating)
        {
            if (Time.time - retreatStartTime < retreatDuration)
            {
                // Vừa lùi vừa bắn
                Vector2 retreatDir = (transform.position - player.position).normalized;
                Vector3 retreatTarget = transform.position + (Vector3)(retreatDir * retreatSpeed * Time.deltaTime);
                movement.MoveTowards(retreatTarget);
                animator.SetBool("isMoving", true);

                // Bắn khi đủ gần
                if (distance <= attackRange && Time.time - lastShootTime >= attackCooldown)
                {
                    lastShootTime = Time.time;
                    Shoot();
                }

                return;
            }
            else
            {
                isRetreating = false;
                movement.Stop();
                animator.SetBool("isMoving", false);
            }
        }

        if (distance < preferredDistance - 0.2f)
        {
            isRetreating = true;
            retreatStartTime = Time.time;
        }
        else
        {
            movement.Stop();
            animator.SetBool("isMoving", false);

            // Bắn nếu đứng yên trong khoảng tấn công
            if (distance <= attackRange && Time.time - lastShootTime >= attackCooldown)
            {
                lastShootTime = Time.time;
                Shoot();
            }
        }
    }



    void HandlePatrol()
    {
        patrol.UpdatePatrol();
        bool isMoving = !patrol.IsWaiting;
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            Vector2 dir = (patrol.CurrentTarget - (Vector2)transform.position).normalized;
            UpdateFacing(dir);
            movement.MoveTowards(patrol.CurrentTarget);
        }
        else
        {
            movement.Stop();
        }
    }

    void UpdateFacing(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetFloat("x", direction.x > 0 ? 1 : -1);
            animator.SetFloat("y", 0);
            animator.SetFloat("huong", direction.x > 0 ? 4 : 2);
        }
        else
        {
            animator.SetFloat("x", 0);
            animator.SetFloat("y", direction.y > 0 ? 1 : -1);
            animator.SetFloat("huong", direction.y > 0 ? 3 : 1);
        }

        if (gun != null)
        {
            Vector2 dir = direction.normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            gun.rotation = Quaternion.Euler(0, 0, angle - 180f);

            gun.localScale = new Vector3(1, Mathf.Abs(angle) > 90f ? 1 : -1, 1);
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Vector2 shootDirection = (player.position - firePoint.position).normalized;

            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(shootDirection);
            }
        }
    }
    void ShootCircle()
    {
        if (bulletPrefab == null || firePoint == null) return;

        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(direction);
            }

            angle += angleStep;
        }

        Debug.Log("💥 Boss shot circle bullets!");
    }

}
