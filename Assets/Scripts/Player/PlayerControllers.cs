using UnityEngine;
using UnityEngine.UI;

public class PlayerControllers : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator animator;

    private float inputH;
    private float inputV;
    private Vector2 moveInput;
    private Vector2 huongCuoi;

    public float speedMove = 5f;
    private bool isAttacking = false;
    private bool isKnockbacked = false;

    [Header("Attack Settings")]
    public float attackRange = 1f;
    public int damage = 10;
    public LayerMask enemyLayer;

    [Header("Mana Settings")]
    public float currentMP = 10f;
    public float maxMP = 10f;
    public int mpCostPerShot = 3;
    public float mpRegenInterval = 0.5f;
    private float mpRegenTimer = 0f;
    public Image manaFillImage; // Kéo Image fill vào đây

    [Header("Magic Bullet")]
    public GameObject magicBulletPrefab;
    public float bulletSpeed = 10f;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        currentMP = maxMP;
        mpRegenTimer = mpRegenInterval;
    }

    [System.Obsolete]
    void Update()
    {
        if (!isAttacking)
        {
            // Điều khiển di chuyển
            inputH = Input.GetAxisRaw("Horizontal");
            inputV = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(inputH, inputV).normalized;

            if (inputH != 0)
                huongCuoi = new Vector2(inputH, 0);
            else if (inputV != 0)
                huongCuoi = new Vector2(0, inputV);

            animator.SetFloat("Move X", inputH);
            animator.SetFloat("Move Y", inputV);
            animator.SetBool("Move", moveInput.magnitude > 0);

            if (huongCuoi.y > 0) animator.SetFloat("direction", 2);
            else if (huongCuoi.y < 0) animator.SetFloat("direction", 0);
            else if (huongCuoi.x < 0) animator.SetFloat("direction", 1);
            else if (huongCuoi.x > 0) animator.SetFloat("direction", 3);

            // Đánh cận chiến bằng X
            if (Input.GetKeyDown(KeyCode.X))
            {
                currentMP -= 0.7f;
                currentMP = Mathf.Clamp(currentMP, 0, maxMP);

                isAttacking = true;
                animator.SetTrigger("Attack");

                float atkX = Mathf.RoundToInt(huongCuoi.x);
                float atkY = Mathf.RoundToInt(huongCuoi.y);

                animator.SetFloat("Attack X", atkX);
                animator.SetFloat("Attack Y", atkY);

                PerformAttack();
                Invoke(nameof(EndAttack), 0.4f);
            }

// Bắn phép bằng C nếu đủ MP
            if (Input.GetKeyDown(KeyCode.C) && currentMP >= mpCostPerShot)
            {
                ShootMagic();
                currentMP -= mpCostPerShot;
                Debug.Log("Player bắn phép! MP còn lại: " + currentMP);
            }


// Hồi MP mỗi 0.5s
            if (currentMP < maxMP)
            {
                mpRegenTimer -= Time.deltaTime;
                if (mpRegenTimer <= 0f)
                {
                    currentMP += 1;
                    currentMP = Mathf.Clamp(currentMP, 0, maxMP);
                    mpRegenTimer = mpRegenInterval;
                    Debug.Log("Hồi 1 MP. MP hiện tại: " + currentMP);
                }
            }

            // Cập nhật thanh mana
            if (manaFillImage != null)
            {
                manaFillImage.fillAmount = (float)currentMP / maxMP;
            }
        }
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        if (!isAttacking && !isKnockbacked)
            rig.velocity = moveInput * speedMove;
        else
            rig.velocity = Vector2.zero;
    }

    void EndAttack()
    {
        isAttacking = false;
        animator.ResetTrigger("Attack");
        animator.Play("Blend Idle");
    }

    [System.Obsolete]
    void PerformAttack()
    {
        Vector2 attackDir = huongCuoi.normalized;
        Vector2 attackPos = (Vector2)transform.position + attackDir * 0.7f;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth hp = enemy.GetComponent<EnemyHealth>();
            if (hp != null)
            {
                Vector2 knockbackDir = (enemy.transform.position - transform.position).normalized;
                float knockbackForce = 5f;
                hp.TakeDamage(damage, knockbackDir, knockbackForce);
            }
        }
    }

    [System.Obsolete]
    void ShootMagic()
    {
        if (magicBulletPrefab == null) return;

        Vector2 direction = huongCuoi.normalized;
        Vector2 spawnPos = (Vector2)transform.position + direction * 0.6f;

        GameObject bullet = Instantiate(magicBulletPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
    }

    [System.Obsolete]
    public void ApplyKnockback(Vector2 force)
    {
        isKnockbacked = true;
        rig.velocity = Vector2.zero;
        rig.AddForce(force, ForceMode2D.Impulse);
        Invoke(nameof(ResetKnockback), 0.2f);
    }

    void ResetKnockback()
    {
        isKnockbacked = false;
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        Vector2 attackDir = huongCuoi.normalized;
        Vector2 attackPos = (Vector2)transform.position + attackDir * 0.7f;
        Gizmos.DrawWireSphere(attackPos, attackRange);
    }
}
