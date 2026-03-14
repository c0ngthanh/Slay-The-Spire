using UnityEngine;
using System;
using System.Collections.Generic;


public enum CombatPhase{
    PlayerTurn,
    EnemyTurn
}
public class CombatSystem : SystemBase
{
    // This information will be used to manage the combat state

    private CombatModel combatModel => GameModel.Instance.GetModel<CombatModel>();

    public override void Initialize(){
    }
    public override void Dispose()
    {
        //;
    }

    public void StartCombat(List<CombatUnit> firstTeam, List<CombatUnit> secondTeam){
        combatModel.PhaseUnits[CombatPhase.PlayerTurn] = firstTeam;
        combatModel.PhaseUnits[CombatPhase.EnemyTurn] = secondTeam;
        EventBusSystem.Publish(new CombatStartEvent());
        combatModel.CurrentTurn = 0;
        SetTeamPosition();
        StartTurn();
    }

    private void SetTeamPosition()
    {
        for(int i = 0; i < combatModel.PhaseUnits[CombatPhase.PlayerTurn].Count; i++)
        {
            combatModel.PhaseUnits[CombatPhase.PlayerTurn][i].SetPosition(new Vector3(-(3*i+4), 0, 0));
        }
        for(int i = 0; i < combatModel.PhaseUnits[CombatPhase.EnemyTurn].Count; i++)
        {
            combatModel.PhaseUnits[CombatPhase.EnemyTurn][i].SetPosition(new Vector3(3*i+2, 0, 0));
        }
    }

    private void StartTurn(){
        EventBusSystem.Publish(new TurnStartEvent());
        // wait for player input or AI decision
        combatModel.CurrentTurn += 1;
        if(combatModel.CurrentPhase == CombatPhase.PlayerTurn){
            // Player Turn Logic
            Debug.Log("Player Turn Logic");
        }
        else
        {
            Debug.Log("Enemy Turn Logic");
            EndTurn(new EndTurnEvent());
        }
        EventBusSystem.Publish(new CombatInfoEvent(combatModel.CurrentPhase.ToString(), combatModel.PhaseUnits[CombatPhase.PlayerTurn], combatModel.PhaseUnits[CombatPhase.EnemyTurn]));
    }

    private void EndTurn(EndTurnEvent @event){
        EventBusSystem.Publish(new TurnEndEvent());
        if(combatModel.CurrentPhase == CombatPhase.PlayerTurn){
            combatModel.CurrentPhase = CombatPhase.EnemyTurn;
        }
        else
        {
            combatModel.CurrentPhase = CombatPhase.PlayerTurn;
        }
        StartTurn();
    }

    private void CheckForEndCombat()
    {
        bool firstTeamAlive = false;
        foreach(var unit in combatModel.PhaseUnits[CombatPhase.PlayerTurn]){
            if(unit.Attribute.HP > 0){
                firstTeamAlive = true;
                break;
            }
        }
        bool secondTeamAlive = false;
        foreach(var unit in combatModel.PhaseUnits[CombatPhase.EnemyTurn]){
            if(unit.Attribute.HP > 0){
                secondTeamAlive = true;
                break;
            }
        }
        if(!firstTeamAlive || !secondTeamAlive){
            EventBusSystem.Publish(new CombatEndEvent());
        }
    }

    internal void RequestEndTurn()
    {
        EventBusSystem.Publish(new EndTurnEvent());
        EndTurn(new EndTurnEvent());
    }
}
