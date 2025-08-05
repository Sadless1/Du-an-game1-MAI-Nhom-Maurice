using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.03f;

    private string[] lines;
    private int index;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private bool dialogueEnded = false;

    void Update()
    {
        if (!dialoguePanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isTyping)
            {
                // Nếu đang gõ chữ → hiện hết luôn dòng hiện tại
                StopCoroutine(typingCoroutine);
                dialogueText.text = lines[index];
                isTyping = false;
            }
            else
            {
                // Nếu đã gõ xong
                if (!dialogueEnded && index == lines.Length - 1)
                {
                    dialogueEnded = true;
                    dialogueText.text = "Được rồi, chúc bạn chơi vui vẻ!";
                    LoadIntro(); // Chuyển scene ngay lập tức
                }
                else
                {
                    ShowNextLine();
                }
            }
        }

        // Nhấn X để thoát game nếu ở dòng cuối
        if (!isTyping && index == lines.Length - 1 && !dialogueEnded)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }

    public void StartDialogue(string[] newLines)
    {
        lines = newLines;
        index = 0;
        dialogueEnded = false;
        dialoguePanel.SetActive(true);
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in lines[index])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void ShowNextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

    void LoadIntro()
    {
        SceneManager.LoadScene("IntroDos");
    }
}
