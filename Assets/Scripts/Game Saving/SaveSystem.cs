using UnityEngine;
using System.IO;
using System.Text;
using System;

public static class SaveSystem
{
    private static string GetPath(int slot) => Path.Combine(Application.persistentDataPath, $"save_{slot}.dat");

    public static void Save(GameData data, int slot)
    {
        string json = JsonUtility.ToJson(data, true);
        string encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        File.WriteAllText(GetPath(slot), encoded);
    }

    public static GameData Load(int slot)
    {
        string path = GetPath(slot);
        if (!File.Exists(path)) return new GameData();

        string encoded = File.ReadAllText(path);
        string json = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
        return JsonUtility.FromJson<GameData>(json);
    }

    public static Boolean hasSlotData(int slot)
    {
        string path = GetPath(slot);
        return File.Exists(path);
    }
}