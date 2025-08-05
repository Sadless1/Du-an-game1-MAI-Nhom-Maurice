using UnityEngine;
using UnityEngine.SceneManagement;

public class level1goto : MonoBehaviour
{
    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
