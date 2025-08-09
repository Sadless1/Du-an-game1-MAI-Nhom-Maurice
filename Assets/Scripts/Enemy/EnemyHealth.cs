using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 10;
    private int currentHP;
    private bool isDead = false;

    [Header("Components")]
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Drop Settings")]
    public GameObject healthItemPrefab;
    [Range(0f, 1f)] public float dropRate = 0.7f;

    [Header("Health UI")]
    public Slider healthSlider;

    private Coroutine flashRoutine;

    void Start()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = currentHP;
        }
    }

    [System.Obsolete]
    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce = 5f)
    {
        if (isDead) return;

        currentHP -= damage;
        if (healthSlider != null)
        {
            healthSlider.value = currentHP;
        }

        Debug.Log(name + " bị trúng đòn! HP còn: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            FlashRed();
            ApplyKnockback(knockbackDirection, knockbackForce);
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true; // Đặt ngay đầu để chặn toàn bộ hành động khác

        // Ngừng mọi di chuyển và tấn công
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true; // Chặn vật lý
        }

        // Nếu có AI khác điều khiển
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this) script.enabled = false; // Tắt các script khác
        }
        // Hủy gun nếu có
        EnemyB enemyB = GetComponent<EnemyB>();
        if (enemyB != null && enemyB.gun != null)
        {
            Destroy(enemyB.gun.gameObject);
        }
        TryDropHealthItem();

        if (animator != null)
        {
            animator.ResetTrigger("Attack"); // Chặn animation tấn công tiếp
            animator.SetTrigger("Die");
            Destroy(gameObject, 1.2f);
        }
        else
        {
            Destroy(gameObject);
        }

        if (healthSlider != null)
        {
            Destroy(healthSlider.gameObject);
        }

        Debug.Log(name + " đã chết");
    }

    void TryDropHealthItem()
    {
        if (healthItemPrefab != null && Random.value < dropRate)
        {
            Instantiate(healthItemPrefab, transform.position, Quaternion.identity);
        }
    }

    private void FlashRed()
    {
        if (flashRoutine != null)
        {
StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(DoFlashRed());
    }

    private IEnumerator DoFlashRed()
    {
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = originalColor;
        }
        flashRoutine = null;
    }

    [System.Obsolete]
    public void ApplyKnockback(Vector2 direction, float force)
    {
        if (rb != null && !isDead)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }
    }
}