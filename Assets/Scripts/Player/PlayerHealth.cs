using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("GameOver")]
    public GameOver GO;
    
    [Header("Health Settings")]
    public int maxHP = 10;
    private int currentHP;
    private bool isDead = false;

    [Header("Components")]
    private Animator animator;
    private PlayerControllers playerController;
    private SpriteRenderer spriteRenderer;

    [Header("UI")]
    public Image hpFillImage;

    // Quản lý nháy đỏ
    private Coroutine flashRoutine;
    private float flashDuration = 0.1f;
    private float flashTimer = 0f;

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerControllers>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (hpFillImage != null)
            hpFillImage.fillAmount = 1f;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        Debug.Log("Player mất " + damage + " HP. Còn lại: " + currentHP);

        if (hpFillImage != null)
            hpFillImage.fillAmount = (float)currentHP / maxHP;

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            FlashRed();
        }
    }

    void Die()
    {
        isDead = true;

        if (animator != null)
            animator.SetTrigger("Die");

        if (playerController != null)
            playerController.enabled = false;

        // Gọi UI Game Over
        if (GO != null)
            GO.gameOver();

        // Ẩn sprite thay vì destroy Player
        spriteRenderer.enabled = false; 
        GetComponent<Collider2D>().enabled = false;
    }



    public bool IsDead() => isDead;

    private void FlashRed()
    {
        flashTimer = flashDuration; // reset thời gian đỏ
        if (flashRoutine == null)
            flashRoutine = StartCoroutine(DoFlashRed());
    }

    private IEnumerator DoFlashRed()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        while (flashTimer > 0f)
        {
            flashTimer -= Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = originalColor;
        flashRoutine = null;
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        Debug.Log("Player hồi " + amount + " HP. Tổng: " + currentHP);

        if (hpFillImage != null)
            hpFillImage.fillAmount = (float)currentHP / maxHP;
    }
}