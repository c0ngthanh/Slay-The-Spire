using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance = null;

    private Dictionary<Type, SystemBase> systemDic = new Dictionary<Type, SystemBase>();

    //Temp config for combat
    [SerializeField] private CombatAttribute[] firstTeam;
    [SerializeField] private CombatAttribute[] secondTeam;
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

    private void Initialize()
    {
        // Initialize all systems here if needed
        systemDic.Add(typeof(EffectSystem), new EffectSystem());
        systemDic.Add(typeof(CombatSystem), new CombatSystem());
        InitalizeAllSystems();
    }

    private void InitalizeAllSystems()
    {
        foreach (var system in systemDic.Values)
        {
            system.Initialize();
        }
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
    
    private void Start(){
        // Temp code to start combat for testing
        CombatUnit[] firstTeamUnits = new CombatUnit[firstTeam.Length];
        for(int i = 0; i < firstTeam.Length; i++){
            Debug.Log("First Team Unit " + i + " Attribute HP: " + firstTeam[i].MaxHPBase);
            firstTeamUnits[i] = new CombatUnit(firstTeam[i]);
        }
        CombatUnit[] secondTeamUnits = new CombatUnit[secondTeam.Length];
        for(int i = 0; i < secondTeam.Length; i++){
            Debug.Log("Second Team Unit " + i + " Attribute HP: " + secondTeam[i].MaxHPBase);
            secondTeamUnits[i] = new CombatUnit(secondTeam[i]);
        }
        GetSystem<CombatSystem>().StartCombat(firstTeamUnits, secondTeamUnits);
    }
}
