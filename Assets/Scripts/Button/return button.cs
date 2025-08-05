using UnityEngine;
using UnityEngine.SceneManagement;

public class returnbutton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ReturnMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
