using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    // Singleton access if we want to call it from everywhere without passing GameSystem around
    // Or we provide it through GameSystem. Let's make it accessible globally since the user asked for it to be "called in every where in project".
    public static GameData Instance { get; private set; }

    private Dictionary<Type, SystemData> dataDic = new Dictionary<Type, SystemData>();

    public GameData()
    {
        Instance = this;
    }

    public T GetSystemData<T>() where T : SystemData, new()
    {
        Type type = typeof(T);
        if (!dataDic.ContainsKey(type))
        {
            Debug.Log("Create New System Data: " + type.ToString());
            T data = new T();
            dataDic[type] = data;
        }
        return dataDic[type] as T;
    }

    public void RegisterData<T>(T data) where T : SystemData
    {
        Type type = typeof(T);
        dataDic[type] = data;
    }

    // Method to load GameData from a save file (future expansion)
    // public static void Load(GameData savedData) { Instance = savedData; }
}
