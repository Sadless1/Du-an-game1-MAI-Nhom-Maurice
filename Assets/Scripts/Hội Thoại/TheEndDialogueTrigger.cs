using UnityEngine;

public class  TheEndDialogueTrigger : MonoBehaviour
{
    public  TheEndDialogueManager dialogueManager;

    void Start()
    {
        string[] intro = new string[]
        {
            "Chúc mừng bạn đã hoàn thành trò chơi",

            "Tạm biệt và hẹn gặp lại",
            "[Mọi dự liệu đã được tải về máy bạn]",
            "[Chuẩn bị thoát ra ngoài desktop]",
        };

        dialogueManager.StartDialogue(intro);
    }
}