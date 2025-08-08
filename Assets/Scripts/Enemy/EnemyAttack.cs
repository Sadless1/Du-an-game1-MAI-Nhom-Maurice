using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator animator;
    private Transform player;

    [Header("Attack Settings")]
    public float attackRange = 1f;
    public float attackCooldown = 1.5f;
    public int damage = 10; // ✅ THÊM VÀO ĐÂY

    private bool isAttacking = false;
    private float lastAttackTime = -Mathf.Infinity;

    public bool IsAttacking => isAttacking;

    public void Init(Animator anim, Transform target)
    {
        animator = anim;
        player = target;
    }

    public bool CanAttack()
    {
        if (player == null) return false;
        float distance = Vector2.Distance(transform.position, player.position);
        return distance <= attackRange;
    }

    public void TryAttack()
    {
        if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
        {
            isAttacking = true;
            lastAttackTime = Time.time;
            animator.SetBool("isAttacking", true);
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    public void FaceTarget()
    {
        if (player == null) return;
        Vector2 direction = (player.position - transform.position).normalized;

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

    [System.Obsolete]
    public void ApplyDamageToPlayer()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                // Gọi hàm ApplyKnockback nếu có
                Vector2 knockbackDir = (player.position - transform.position).normalized;
                float knockbackForce = 5f;

                PlayerControllers playerCtrl = player.GetComponent<PlayerControllers>();
                if (playerCtrl != null)
                {
                    playerCtrl.ApplyKnockback(knockbackDir * knockbackForce);
                }
            }
        }
    }



}
