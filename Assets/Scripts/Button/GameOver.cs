using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverUI;
    public void gameOver()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;        // Dừng game
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Đảm bảo lúc bắt đầu game thì thời gian chạy bình thường
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}