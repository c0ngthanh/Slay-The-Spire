using UnityEngine;
using System;
using System.Collections.Generic;

public enum CombatEvent{
    StartCombat, 
    StartTurn,
    EndTurn,
    EndCombat
}

public enum CombatPhase{
    PlayerTurn,
    EnemyTurn
}
public class CombatSystem : SystemBase
{
    // This information will be used to manage the combat state
    public Dictionary<CombatPhase, List<CombatUnit>> PhaseUnits = new ();
    public int CurrentTurn;
    public CombatUnit currentUnit;
    private CombatPhase currentPhase = CombatPhase.PlayerTurn;

    public Dictionary<CombatEvent, Action> CombatEventDic = new ();
    public override void Initialize(){
        CombatEventDic[CombatEvent.StartCombat] = () => {};
        CombatEventDic[CombatEvent.StartTurn] = () => {};
        CombatEventDic[CombatEvent.EndTurn] = () => {};
        CombatEventDic[CombatEvent.EndCombat] = () => {};

        EventBusSystem.Subscribe<EndTurnEvent>(EndTurn);
    }
    public override void Dispose()
    {
        //;
    }

    public Action GetActionByType(CombatEvent eventType){
        if(CombatEventDic.ContainsKey(eventType)){
            return CombatEventDic[eventType];
        }else{
            Debug.LogError("CombatSystem: No Action found for event type " + eventType.ToString());
            return null;
        }
    }

    public void StartCombat(List<CombatUnit> firstTeam, List<CombatUnit> secondTeam){
        PhaseUnits[CombatPhase.PlayerTurn] = firstTeam;
        PhaseUnits[CombatPhase.EnemyTurn] = secondTeam;
        CombatEventDic[CombatEvent.StartCombat]?.Invoke();
        CurrentTurn = 0;
        StartTurn();
        SetTeamPosition();
    }

    private void SetTeamPosition()
    {
        for(int i = 0; i < PhaseUnits[CombatPhase.PlayerTurn].Count; i++)
        {
            PhaseUnits[CombatPhase.PlayerTurn][i].SetPosition(new Vector3(-(3*i+4), 0, 0));
        }
        for(int i = 0; i < PhaseUnits[CombatPhase.EnemyTurn].Count; i++)
        {
            PhaseUnits[CombatPhase.EnemyTurn][i].SetPosition(new Vector3(3*i+2, 0, 0));
        }
    }

    private void StartTurn(){
        CombatEventDic[CombatEvent.StartTurn]?.Invoke();
        // wait for player input or AI decision
        CurrentTurn += 1;
        if(currentPhase == CombatPhase.PlayerTurn){
            // Player Turn Logic
            Debug.Log("Player Turn Logic");
        }
        else
        {
            Debug.Log("Enemy Turn Logic");
            EndTurn(new EndTurnEvent());
        }
        EventBusSystem.Publish(new CombatInfoEvent(currentPhase.ToString(), PhaseUnits[CombatPhase.PlayerTurn], PhaseUnits[CombatPhase.EnemyTurn]));
    }

    private void EndTurn(EndTurnEvent endTurnEvent){
        CombatEventDic[CombatEvent.EndTurn]?.Invoke();
        if(currentPhase == CombatPhase.PlayerTurn){
            currentPhase = CombatPhase.EnemyTurn;
        }
        else
        {
            currentPhase = CombatPhase.PlayerTurn;
        }
        StartTurn();
    }

    private void CheckForEndCombat()
    {
        bool firstTeamAlive = false;
        foreach(var unit in PhaseUnits[CombatPhase.PlayerTurn]){
            if(unit.Attribute.HP > 0){
                firstTeamAlive = true;
                break;
            }
        }
        bool secondTeamAlive = false;
        foreach(var unit in PhaseUnits[CombatPhase.EnemyTurn]){
            if(unit.Attribute.HP > 0){
                secondTeamAlive = true;
                break;
            }
        }
        if(!firstTeamAlive || !secondTeamAlive){
            CombatEventDic[CombatEvent.EndCombat]?.Invoke();
        }
    }
}
