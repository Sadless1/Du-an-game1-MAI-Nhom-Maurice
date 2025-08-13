using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FakeDOS : MonoBehaviour
{
    public TextMeshProUGUI dosText;
    public float lineDelay = 0.4f;       // thời gian giữa mỗi dòng
    public float autoSkipTime = 10f;     // skip sau 30 giây
    public string nextScene = "TestMenu";

    private bool skipped = false;

    private string[] lines = new string[]
    {
        "fakeDos 54.75 activated",
        "...",
        "D:/VeryOldgames/MAI/MAI.exe",
        "loading...",

        "******************************",
        "*           [M.A.I]          *",
        "*                            *",
        "* Sản phẩm của nhóm [Maurice]*",
        "*           c2025            *",
        "******************************",
        "initializing brain engine...",
        "render mode: software",
        "checking sound card...",
        "Cant fix the bug [im sorry :( ]",
        "borring music ...",
        "MAI is connecting to your machine",
        "MAI is starting the game",
        "",
        "All done.",
        "Have fun ,[Six]  :) ."
    };

    void Start()
    {
        dosText.text = "";
        StartCoroutine(ShowLinesOneByOne());
        StartCoroutine(AutoSkipAfterTime());
    }

    void Update()
    {
        if (!skipped && Input.anyKeyDown)
        {
            SkipToNextScene();
        }
    }

    IEnumerator ShowLinesOneByOne()
    {
        foreach (string line in lines)
        {
            dosText.text += line + "\n";
            yield return new WaitForSeconds(lineDelay);
        }
    }

    IEnumerator AutoSkipAfterTime()
    {
        yield return new WaitForSeconds(autoSkipTime);
        SkipToNextScene();
    }

    void SkipToNextScene()
    {
        if (skipped) return;
        skipped = true;
        SceneManager.LoadScene(nextScene);
    }
}
