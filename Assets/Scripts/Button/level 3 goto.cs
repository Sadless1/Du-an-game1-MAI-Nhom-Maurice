using UnityEngine;
using UnityEngine.SceneManagement;

public class level3goto : MonoBehaviour
{
    public void PlayLevel3()
    {
        SceneManager.LoadSceneAsync(5);
    }
}
