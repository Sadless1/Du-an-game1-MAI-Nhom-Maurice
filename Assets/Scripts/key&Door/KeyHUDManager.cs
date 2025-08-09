using UnityEngine;

public class KeyHUDManager : MonoBehaviour
{
    [SerializeField] private GameObject[] keyIcons; // Các icon chìa trên HUD

    public void UpdateKeys(int keyCount)
    {
        for (int i = 0; i < keyIcons.Length; i++)
        {
            keyIcons[i].SetActive(i < keyCount); // Bật icon nếu đủ chìa
        }
    }
}