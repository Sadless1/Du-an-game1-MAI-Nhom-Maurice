using UnityEngine;
using UnityEngine.SceneManagement;

public class playbutton : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
