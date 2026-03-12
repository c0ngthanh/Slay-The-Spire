using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private Dictionary<Type, SystemBase> systemDic = new Dictionary<Type, SystemBase>();

    //Temp config for combat
    [SerializeField] private CombatAttribute[] firstTeam;
    [SerializeField] private CombatAttribute[] secondTeam;
    [SerializeField] private List<CardSO> cardSOList;
    [SerializeField] private List<RelicSO> relicSOList;
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
        systemDic.Add(typeof(CardSystem), new CardSystem());
        systemDic.Add(typeof(TargetingSystem), new TargetingSystem());
        systemDic.Add(typeof(UnitSystem), new UnitSystem());
        systemDic.Add(typeof(RelicSystem), new RelicSystem());
        systemDic.Add(typeof(UISystem), new UISystem());
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
            new GameModel(); // Initialize global Data Holder without breaking existing logic
            Initialize();
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
            return;
        }
    }

    private void Update()
    {
        foreach (var system in systemDic.Values)
        {
            system.Tick();
        }
    }

    private void Start(){
        // Temp code to start combat for testing
        List<CombatUnit> firstTeamUnits = new List<CombatUnit>();
        for(int i = 0; i < firstTeam.Length; i++){
            Debug.Log("First Team Unit " + i + " Attribute HP: " + firstTeam[i].MaxHPBase);
            firstTeamUnits.Add(GetSystem<UnitSystem>().CreateCombatUnits(firstTeam[i])); // Create units using UnitSystem and add to list
        }
        List<CombatUnit> secondTeamUnits = new List<CombatUnit>();
        for(int i = 0; i < secondTeam.Length; i++){
            Debug.Log("Second Team Unit " + i + " Attribute HP: " + secondTeam[i].MaxHPBase);
            secondTeamUnits.Add(GetSystem<UnitSystem>().CreateCombatUnits(secondTeam[i]));
        }
        GetSystem<CardSystem>().InitDeck(cardSOList);

        GetSystem<CombatSystem>().StartCombat(firstTeamUnits, secondTeamUnits);

        if (relicSOList != null)
        {
            foreach (var relicSO in relicSOList)
            {
                GetSystem<RelicSystem>().AddRelic(relicSO);
                // Temporarily add a replica into the separated data to show it's working
                GameModel.Instance.GetModel<RelicSystemData>().ownedRelics.Add(new RelicRuntime(relicSO));
            }
        }
        GetSystem<UISystem>().ShowUI<CombatUI>();

        // Test the Relic Data Separator approach
        GetSystem<RelicSystem>().ExampleUseRelicData();
    }
}
