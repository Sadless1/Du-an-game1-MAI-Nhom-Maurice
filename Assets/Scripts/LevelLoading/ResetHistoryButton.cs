using UnityEngine;

public class ResetHistoryButton : MonoBehaviour
{
    public void ResetHistory()
    {
        // Reset CompletedLevel về 0
        PlayerPrefs.SetInt("CompletedLevel", 0);
        PlayerPrefs.Save();

        // Nếu HistoryManager đang tồn tại thì cập nhật lại giao diện
        if (HistoryManager.Instance != null)
            HistoryManager.Instance.UpdateHistory();

        Debug.Log("[ResetHistoryButton] Đã reset toàn bộ HistoryFolder.");
    }
}