using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private KeyHUDManager hudManager;
    [SerializeField] private int keyCount = 0;

    public void AddKey()
    {
        keyCount++;
        hudManager.UpdateKeys(keyCount);
    }

    // Chỉ kiểm tra, không trừ chìa
    public bool HasKeys(int amount)
    {
        return keyCount >= amount;
    }
    
    // public bool UseKeys(int amount)
    // {
    //     if (keyCount >= amount)
    //     {
    //         keyCount -= amount;
    //         hudManager.UpdateKeys(keyCount);
    //         return true;
    //     }
    //     return false;
    // }
}