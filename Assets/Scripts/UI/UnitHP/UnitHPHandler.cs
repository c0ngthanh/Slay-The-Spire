using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitHPHandler : MonoBehaviour
{   
    [SerializeField] private HPBar hpBar;

    private List<HPBar> hpBarPool = new List<HPBar>();

    private int currentActiveHPBars = 0;
    
    private Vector3 offset = new Vector3(0, -1, 0); // Offset above the unit
    void Awake()
    {
        // Subscribe to HP change events if needed
        EventBusSystem.Subscribe<CombatInfoEvent>(SpawnAllHpBar); // just use to spawn, active or deactive HPBar
    }
    private void SpawnAllHpBar(CombatInfoEvent @event)
    {
        //Return all HP bars to the pool before spawning new ones
        foreach (var hpBar in hpBarPool){
            hpBar.gameObject.SetActive(false);
        }
        currentActiveHPBars = 0;
        foreach (var unit in @event.PlayerTeam)
        {
            SpawnSingleBar(unit);
        }

        foreach (var unit in @event.EnemyTeam)
        {
            SpawnSingleBar(unit);
        }
    }


    private void SpawnSingleBar(CombatUnit unit)
    {
        HPBar hpBarInstance;
        if (hpBarPool.Count > currentActiveHPBars)
        {
            hpBarInstance = hpBarPool[currentActiveHPBars];
            hpBarInstance.gameObject.SetActive(true);
            currentActiveHPBars++;
        }
        else
        {
            hpBarInstance = Instantiate(hpBar, transform);
            hpBarPool.Add(hpBarInstance);
            currentActiveHPBars++;
        }
        hpBarInstance.SetUnitUID(unit.UID);
        hpBarInstance.UpdateHPBar(unit.Attribute.HP, unit.Attribute.MaxHP);
        hpBarInstance.transform.position = Camera.main.WorldToScreenPoint(unit.GetWorldPosition() + offset);
    }
}
