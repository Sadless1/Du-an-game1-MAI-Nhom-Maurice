using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        string[] intro = new string[]
        {
            "Xin chào, người chơi.",
            "Mình là MAI.",
            "Viết tắt của [Memorize And Improve]",
            "Mình là 1 chương trình dạy học vào năm 1995.",
            "Rất lâu trước khi con người các bạn đã chinh phục các vì sao.",
            "Mình rất xin lỗi phải nói rằng nhiều chương trình học tập của mình đã bị [Corrupt]",
            "Tuy nhiên vẫn còn 1 chương trình.",
            "Chương trình này bạn sẽ nhập vai vào một người lính đánh nhau với kẻ địch.",
            "Ở cuối màn là thư mục chứa mọi thứ xảy ra vào năm 1954–1975.",
            "Bạn có muốn chạy chương trình đó không? Z(yes)/X(no)"
        };

        dialogueManager.StartDialogue(intro);
    }
}