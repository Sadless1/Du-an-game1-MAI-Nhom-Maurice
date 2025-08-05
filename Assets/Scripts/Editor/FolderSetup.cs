using UnityEditor;
using UnityEngine;
using System.IO;

public class FolderSetup : MonoBehaviour
{
    [MenuItem("Tools/Setup Default Folders")]
    static void CreateFolders()
    {
        string[] folders = {
            "Assets/Animation",
            "Assets/Audio",
            "Assets/Fonts",
            "Assets/Import",
            "Assets/Levels",
            "Assets/Prefabs",
            "Assets/Scripts",
            "Assets/Settings",
            "Assets/TextMeshPro",
            "Assets/TilesMap"
        };

        foreach (string folder in folders)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        AssetDatabase.Refresh();
        Debug.Log("Default folders created!");
    }
}
