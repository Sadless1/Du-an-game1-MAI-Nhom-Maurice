using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float patrolRadius = 3f;                // Bán kính di chuyển tuần tra
    public float patrolWaitTime = 2f;              // Thời gian đứng chờ giữa 2 lần tuần tra
    public float patrolMoveDuration = 1.5f;

    private Vector2 patrolCenter;
    private Vector2 patrolTarget;

    private float patrolTimer;
    private float waitTimer;

    public bool IsWaiting => patrolTimer >= patrolMoveDuration;
    public Vector2 CurrentTarget => patrolTarget;

    private void Start()
    {
        patrolCenter = transform.position;
        ResetTimers();
        ChooseNewPatrolTarget();
    }

    public void UpdatePatrol()
    {
        if (!IsWaiting)
        {
            // Đang di chuyển
            patrolTimer += Time.deltaTime;
        }
        else
        {
            // Đang chờ trước khi chọn điểm mới
            waitTimer += Time.deltaTime;

            if (waitTimer >= patrolWaitTime)
            {
                ResetTimers();
                ChooseNewPatrolTarget();
            }
        }
    }

    private void ChooseNewPatrolTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * patrolRadius;
        patrolTarget = patrolCenter + randomOffset;
    }

    public void ResetTimers()
    {
        patrolTimer = 0f;
        waitTimer = 0f;
    }
}
