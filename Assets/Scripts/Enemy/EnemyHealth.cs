using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health")]
    public int maxHP = 100;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    // Gọi từ bên ngoài khi enemy nhận sát thương
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    private void Die()
    {
        // Chết: có thể thêm hiệu ứng, drop item, animation, etc.
        Destroy(gameObject);
    }
}
