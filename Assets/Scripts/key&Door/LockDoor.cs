using UnityEngine;

public class LockDoor : MonoBehaviour
{
    [SerializeField] private int requiredKeys = 2;
    [SerializeField] private GameObject doorRoot; // Phần cửa chính để xóa

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
            //if (inventory != null && inventory.HasKeys(requiredKeys))
            if (inventory != null && inventory.HasKeys(requiredKeys))
            {
                Destroy(doorRoot); // Xóa toàn bộ cửa khi mở
            }
        }
    }
}