using System.IO;
using System;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Path.Combine(Application.persistentDataPath, "SaveData");
    
    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string fileName, string saveString)
    {
        string path = Path.Combine(SAVE_FOLDER, fileName);
        byte[] data = System.Text.Encoding.ASCII.GetBytes(saveString);
        System.IO.File.WriteAllBytes(path, data);
    }

    public static string Load(string fileName)
    {
        string path = Path.Combine(SAVE_FOLDER, fileName);
        if (System.IO.File.Exists(path))
        {
            byte[] data = System.IO.File.ReadAllBytes(path);
            string saveString = System.Text.Encoding.ASCII.GetString(data);

            return saveString;
        }
        return null;
    }
}
