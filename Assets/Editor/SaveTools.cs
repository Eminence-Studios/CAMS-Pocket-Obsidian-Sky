using UnityEngine;
using UnityEditor; // This allows us to talk to the Unity software
using System.IO;

public class SaveTools
{
    // The MenuItem attribute tells Unity to create a button at the top
    [MenuItem("CAMS/Clear All Save Data")]
    public static void ClearSaves()
    {
        // 1. Get the folder where the saves live
        string path = Application.persistentDataPath;
        DirectoryInfo di = new DirectoryInfo(path);

        // 2. Loop through every file and delete .dat files
        int count = 0;
        foreach (FileInfo file in di.GetFiles())
        {
            if (file.Extension == ".dat")
            {
                file.Delete();
                count++;
            }
        }

        // 3. Refresh the console so you know it worked
        Debug.Log($"Successfully wiped {count} save files from: {path}");
    }
}