using UnityEngine;

public class EnemyA : MonoBehaviour
{
    private Animator animator;
    private Transform player;
    private EnemyMovement movement;
    private EnemyPatrol patrol;

    [Header("Enemy Settings")]
    public float speed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float attackCooldown = 1.5f;

    private bool isAttacking = false;
    private float lastAttackTime = -Mathf.Infinity;

    private enum EnemyState { Idle, Patrolling, Chasing, Attacking }
    private EnemyState currentState = EnemyState.Patrolling;

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

        if (distance <= attackRange)
            currentState = EnemyState.Attacking;
        else if (distance <= chaseRange)
            currentState = EnemyState.Chasing;
        else
            currentState = EnemyState.Patrolling;

        HandleState();
    }

    void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.Attacking:
                HandleAttack();
                break;
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

    void HandleAttack()
    {
        movement.Stop();
        animator.SetBool("isMoving", false);

        Vector2 direction = (player.position - transform.position).normalized;
        UpdateFacing(direction);

        if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
        {
            isAttacking = true;
            lastAttackTime = Time.time;
            animator.SetBool("isAttacking", true);
        }
    }

    void HandleChase()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);

        Vector2 direction = (player.position - transform.position).normalized;
        UpdateFacing(direction);

        movement.MoveTowards(player.position);
        animator.SetBool("isMoving", true);
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
    }

    void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }
}
