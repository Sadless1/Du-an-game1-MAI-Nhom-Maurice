using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    public static HistoryManager Instance;

    public GameObject HistoryFolder1;
    public GameObject HistoryFolder2;
    public GameObject HistoryFolder3;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateHistory();
    }

    public void UpdateHistory()
    {
        HistoryFolder1.SetActive(false);
        HistoryFolder2.SetActive(false);
        HistoryFolder3.SetActive(false);

        int completedLevel = PlayerPrefs.GetInt("CompletedLevel", 0);
        Debug.Log($"[HistoryManager] CompletedLevel = {completedLevel}");

        if (completedLevel >= 1) HistoryFolder1.SetActive(true);
        if (completedLevel >= 2) HistoryFolder2.SetActive(true);
        if (completedLevel >= 3) HistoryFolder3.SetActive(true);
    }
}