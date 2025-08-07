using UnityEngine;
using UnityEngine.SceneManagement;
public class Wingoto : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GotoWin()
    {
        SceneManager.LoadScene("FakeWInXp");
    }
}
