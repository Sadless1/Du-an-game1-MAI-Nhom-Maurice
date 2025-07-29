using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu chạm vào đối tượng có tag là "NextLevel"
        if (other.CompareTag("NextLevel"))
        {
            // Lấy index hiện tại của scene
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Chuyển sang scene kế tiếp (index + 1)
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
