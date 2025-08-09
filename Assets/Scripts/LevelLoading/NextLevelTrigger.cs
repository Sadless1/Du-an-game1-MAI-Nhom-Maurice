using UnityEngine;
using UnityEngine.SceneManagement; // để load scene

public class NextLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu người chơi chạm
        if (other.CompareTag("Player"))
        {
            // Lấy scene hiện tại
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Chuyển sang scene tiếp theo
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}