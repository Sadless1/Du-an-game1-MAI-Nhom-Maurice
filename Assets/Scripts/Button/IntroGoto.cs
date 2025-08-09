using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroGoto : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void IntroDos()
    {
        SceneManager.LoadScene("IntroMAI");
    }
}