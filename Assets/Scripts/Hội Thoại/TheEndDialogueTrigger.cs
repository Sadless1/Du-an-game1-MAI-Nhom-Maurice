using UnityEngine;

public class  TheEndDialogueTrigger : MonoBehaviour
{
    public  TheEndDialogueManager dialogueManager;

    void Start()
    {
        string[] intro = new string[]
        {
            "Chúc mừng bạn đã hoàn thành trò chơi",
            "Bạn đã có thứ mà mình muốn ",
            "Cảm ơn bạn rất nhiều vì đã chơi trò chơi này mặc dù nó đã bị [hỏng] từ lâu ",
 
            "Tạm biệt và hẹn gặp lại",
            "Thoát ra ngoài desktop[Yes(Z)/No(X)]",
        };

        dialogueManager.StartDialogue(intro);
    }
}