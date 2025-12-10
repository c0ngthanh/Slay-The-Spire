using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance = null;

    private Dictionary<Type, SystemBase> systemDic = new Dictionary<Type, SystemBase>();

    public T GetSystem<T>() where T : SystemBase, new()
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

    public void Initialize()
    {
        // Initialize all systems here if needed
        systemDic.Add(typeof(EffectSystem), new EffectSystem());
        systemDic.Add(typeof(CombatSystem), new CombatSystem());
    }
    
    private void Awake(){
        if(Instance == null){
            Instance = this;
            Initialize();
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
            return;
        }
    }
}
