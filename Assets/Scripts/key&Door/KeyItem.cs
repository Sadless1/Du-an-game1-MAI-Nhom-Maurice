using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KeyItem : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKey();
                Destroy(gameObject); // Xóa chìa trên map
            }
        }
    }
}