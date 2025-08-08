using UnityEngine;

public class EnemyA : MonoBehaviour
{
    private Animator animator;
    private Transform player;
    private EnemyMovement movement;
    private EnemyPatrol patrol;
    private EnemyAttack attack;

    [Header("Enemy Settings")]
    public float speed = 2f;
    public float chaseRange = 5f;

    private enum EnemyState { Idle, Patrolling, Chasing, Attacking }
    private EnemyState currentState = EnemyState.Patrolling;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
        patrol = GetComponent<EnemyPatrol>();
        attack = GetComponent<EnemyAttack>();

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            attack.Init(animator, player);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (attack.CanAttack())
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

        attack.FaceTarget();
        attack.TryAttack();
    }

    void HandleChase()
    {
        animator.SetBool("isMoving", true);
        attack.EndAttack();

        Vector2 direction = (player.position - transform.position).normalized;
        attack.FaceTarget(); // Dùng lại FaceTarget từ Attack script
        movement.MoveTowards(player.position);
    }

    void HandlePatrol()
    {
        attack.EndAttack();
        patrol.UpdatePatrol();

        bool isMoving = !patrol.IsWaiting;
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            Vector2 dir = (patrol.CurrentTarget - (Vector2)transform.position).normalized;
            attack.FaceTarget(); // dùng lại
            movement.MoveTowards(patrol.CurrentTarget);
        }
        else
        {
            movement.Stop();
        }
    }
}
