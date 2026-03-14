using System.Collections.Generic;
using UnityEngine;

public class UnitHPHandler : MonoBehaviour, ISubView
{
    [SerializeField] private HPBar hpBar;

    private SPool<HPBar> hpBarPool;

    private Vector3 offset = new Vector3(0, -1, 0); // Offset above the unit

    private CombatModel combatModel => GameModel.Instance.GetModel<CombatModel>();

    public void Init()
    {
        hpBarPool = new SPool<HPBar>(hpBar, 5, transform);
        // Subscribe to HP change events if needed
        SpawnAllHpBar();
    }

    public void Tick()
    {
    }

    private void SpawnAllHpBar()
    {
        //Return all HP bars to the pool before spawning new ones
        hpBarPool.ReturnAllToPool();

        foreach (var unit in combatModel.PhaseUnits[CombatPhase.PlayerTurn])
        {
            SpawnSingleBar(unit);
        }

        foreach (var unit in combatModel.PhaseUnits[CombatPhase.EnemyTurn])
        {
            SpawnSingleBar(unit);
        }
    }


    private void SpawnSingleBar(CombatUnit unit)
    {
        HPBar hpBarInstance = hpBarPool.Get();

        hpBarInstance.SetUnitUID(unit.UID);
        hpBarInstance.UpdateHPBar(unit.Attribute.HP, unit.Attribute.MaxHP);
        hpBarInstance.transform.position = Camera.main.WorldToScreenPoint(unit.GetWorldPosition() + offset);
    }
}
