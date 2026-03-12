using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameModel
{
    // Singleton access if we want to call it from everywhere without passing GameSystem around
    // Or we provide it through GameSystem. Let's make it accessible globally since the user asked for it to be "called in every where in project".
    public static GameModel Instance { get; private set; }

    private Dictionary<Type, ModelBase> dataDic = new Dictionary<Type, ModelBase>();

    public GameModel()
    {
        Instance = this;
    }

    public T GetModel<T>() where T : ModelBase, new()
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

    public void RegisterModel<T>(T data) where T : ModelBase
    {
        Type type = typeof(T);
        dataDic[type] = data;
    }

    // Method to load GameData from a save file (future expansion)
    // public static void Load(GameData savedData) { Instance = savedData; }
}
