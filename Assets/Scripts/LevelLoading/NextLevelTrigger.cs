using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            int completedLevel = PlayerPrefs.GetInt("CompletedLevel", 0);

            // Xác định số level vừa hoàn thành
            if (sceneName == "Level 1" && completedLevel < 1)
                PlayerPrefs.SetInt("CompletedLevel", 1);
            else if (sceneName == "Level 2" && completedLevel < 2)
                PlayerPrefs.SetInt("CompletedLevel", 2);
            else if (sceneName == "Level 3" && completedLevel < 3)
                PlayerPrefs.SetInt("CompletedLevel", 3);

            PlayerPrefs.Save();
            Debug.Log($"[NextLevelTrigger] Đã lưu CompletedLevel = {PlayerPrefs.GetInt("CompletedLevel")}");

            // Cập nhật HistoryManager nếu có
            if (HistoryManager.Instance != null)
                HistoryManager.Instance.UpdateHistory();

            // Load scene tiếp theo
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}