using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;         // Tốc độ bay của đạn
    public float lifeTime = 2f;      // Thời gian sống của đạn

    private Vector2 moveDirection;   // Hướng bay của đạn

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Di chuyển đạn theo hướng đã set với tốc độ
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    // Gọi từ bên ngoài để truyền hướng di chuyển
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Gây sát thương
            Destroy(gameObject);
        }
    }
}
