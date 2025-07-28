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
    public string nextScene = "Level 1";

    private bool skipped = false;

    private string[] lines = new string[]
    {
        "fakeDOS 54.75 activated",
        "...",
        "c:/games/MAI/MAI.exe",
        "loading...",

        "******************************",
        "*            MAI             *",
        "*    (Memorize And Improve)  *",
        "*                            *",
        "*     Sản phẩm của nhóm 5    *",
        "*           c2125            *",
        "******************************",
        "initializing brain engine...",
        "render mode: software",
        "checking sound card...",

        "MAI is connecting to your brain",
        "MAI is connecting to your dream",
        "MAI is starting the game",

        "Almost There ",
        "Okay",
        "All done.",
        "Have fun ,Six  :) ."
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
