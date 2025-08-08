using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public int damage = 10;
    public LayerMask enemyLayer;
    public float lifetime = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [System.Obsolete]
    void Shoot()
    {
        Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * 10f;

        // Xoay viên đạn theo hướng bắn
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    [System.Obsolete]
    void Update()
    {
        // Tự xoay viên đạn theo hướng bay
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }


    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            EnemyHealth hp = collision.GetComponent<EnemyHealth>();
            if (hp != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                hp.TakeDamage(damage, knockbackDir, 3f);
            }

            Destroy(gameObject);
        }
    }
}