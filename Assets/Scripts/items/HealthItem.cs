using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healAmount = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.IsDead())
            {
                playerHealth.Heal(healAmount);
                Destroy(gameObject); // Xoá vật phẩm sau khi sử dụng
            }
        }
    }
}
