using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        string[] intro = new string[]
        {
            "Xin chào, người chơi.",
            "Mình là MAI,Viết tắt của [Memorize And Improve]",
            "Mình là 1 chương trình dạy học từ rất nhiều năm về trước .",
            "Rất lâu trước khi con người các bạn đã chinh phục các vì sao.",
            "Bạn muốn thu thập những thông tin xảy ra trong quá khứ phải không?",
            "Mình rất xin lỗi phải nói rằng nhiều chương trình học tập của mình đã bị [Corrupt]",
            "Mình ko thể giúp bạn được",
            "....",
            "Tuy nhiên vẫn còn 1 chương trình.",
            "chương trình này chứa thư mục mọi thứ xảy ra vào năm 1954–1975.",
            "Bạn có muốn chạy chương trình đó không?" , 
            "Z(yes)/X(no)"
        };

        dialogueManager.StartDialogue(intro);
    }
}