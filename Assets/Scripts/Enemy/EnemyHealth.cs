using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 30;
    private int currentHP;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;
    [Header("Drop Settings")]
    public GameObject healthItemPrefab;     // Prefab vật phẩm hồi máu
    [Range(0f, 1f)] public float dropRate = 0.5f; // Tỉ lệ rơi (0.5 = 50%)

    [Header("Health UI")]
    public Slider healthSlider; // Gắn slider từ thanh máu vào đây trong Inspector

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
            StartCoroutine(FlashRed());
            ApplyKnockback(knockbackDirection, knockbackForce);
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        TryDropHealthItem(); // gọi trước khi hủy

        if (animator != null)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 1.2f); // đợi animation
        }
        else
        {
            Destroy(gameObject);
        }
        if (healthSlider != null)
        {
            Destroy(healthSlider.gameObject); // Hoặc: healthSlider.gameObject.SetActive(false);
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

    private IEnumerator FlashRed()
    {
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = originalColor;
        }
    }

    [System.Obsolete]
    public void ApplyKnockback(Vector2 direction, float force)
    {
        if (rb != null && !isDead)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }
    }
}
