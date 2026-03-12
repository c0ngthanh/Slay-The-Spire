using System.Collections.Generic;
using UnityEngine;

public class UnitHPHandler : MonoBehaviour
{
    [SerializeField] private HPBar hpBar;

    private SPool<HPBar> hpBarPool;
    private List<HPBar> activeHPBars = new List<HPBar>();

    private Vector3 offset = new Vector3(0, -1, 0); // Offset above the unit
    void Awake()
    {
        hpBarPool = new SPool<HPBar>(hpBar, 5, transform);
        // Subscribe to HP change events if needed
        EventBusSystem.Subscribe<CombatInfoEvent>(SpawnAllHpBar); // just use to spawn, active or deactive HPBar
    }
    private void SpawnAllHpBar(CombatInfoEvent @event)
    {
        //Return all HP bars to the pool before spawning new ones
        hpBarPool.ReturnAllToPool();
        activeHPBars.Clear();

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
        HPBar hpBarInstance = hpBarPool.Get();
        activeHPBars.Add(hpBarInstance);

        hpBarInstance.SetUnitUID(unit.UID);
        hpBarInstance.UpdateHPBar(unit.Attribute.HP, unit.Attribute.MaxHP);
        hpBarInstance.transform.position = Camera.main.WorldToScreenPoint(unit.GetWorldPosition() + offset);
    }
}
