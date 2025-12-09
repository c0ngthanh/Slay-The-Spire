using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameSystem
{
    private static Dictionary<Type, SystemBase> systemDic = new Dictionary<Type, SystemBase>();

    public static T GetSystem<T>() where T : SystemBase, new()
    {
        Type type = typeof(T);
        if (!systemDic.ContainsKey(type))
        {
            Debug.Log("Create New System: " + type.ToString());
            T system = new T();
            systemDic[type] = system;
        }
        return systemDic[type] as T;
    }

    public static void Initialize()
    {
        // Initialize all systems here if needed
        systemDic.Add(typeof(EffectSystem), new EffectSystem());
    }
}
