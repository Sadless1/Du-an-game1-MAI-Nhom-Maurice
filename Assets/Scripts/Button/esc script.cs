using UnityEngine;

public class escscript : MonoBehaviour
{
    public GameObject pauseMenu;
    
    public bool IsPause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPause)
            {
                continueGame();
            }
            else
            {
                pauseGame();
            }
        }
    }
    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPause = true;
    }
    public void continueGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPause = false;
    }
}
