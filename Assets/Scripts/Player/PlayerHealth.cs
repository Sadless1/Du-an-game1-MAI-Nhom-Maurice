using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;
    private int currentHP;
    private bool isDead = false;

    private Animator animator;
    private PlayerControllers playerController;
    private SpriteRenderer spriteRenderer;

    public Image hpFillImage;


    void Start()
    {
        currentHP = maxHP;
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerControllers>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (hpFillImage != null)
            hpFillImage.fillAmount = 1f; // đầy máu
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

        StartCoroutine(FlashRed());
    }



    void Die()
    {
        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 1.2f);
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Debug.Log("Player đã chết!");
    }

    public bool IsDead()
    {
        return isDead;
    }
    private IEnumerator FlashRed()
    {
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
        }
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
