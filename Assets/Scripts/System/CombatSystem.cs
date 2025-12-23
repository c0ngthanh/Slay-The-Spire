using UnityEngine;
using System;
using System.Collections.Generic;

public enum CombatEvent{
    StartCombat,
    StartTurn,
    EndTurn,
    EndCombat
}

public class CombatSystem : SystemBase
{
    // This information will be used to manage the combat state
    public CombatUnit[] firstTeam;
    public CombatUnit[] secondTeam;
    public int CurrentTurn;
    public CombatUnit currentUnit;

    public Dictionary<CombatEvent, Action> CombatEventDic = new ();
    public override void Initialize(){
        CombatEventDic[CombatEvent.StartCombat] = () => {};
        CombatEventDic[CombatEvent.StartTurn] = () => {};
        CombatEventDic[CombatEvent.EndTurn] = () => {};
        CombatEventDic[CombatEvent.EndCombat] = () => {};
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

    public void StartCombat(CombatUnit[] firstTeam, CombatUnit[] secondTeam){
        Debug.Log("CombatSystem: Starting Combat");
        this.firstTeam = firstTeam;
        this.secondTeam = secondTeam;
        CombatEventDic[CombatEvent.StartCombat]?.Invoke();
        CurrentTurn = 0;
        StartPlayerTurn();
        SetTeamPosition();
    }

    private void SetTeamPosition()
    {
        for(int i = 0; i < firstTeam.Length; i++)
        {
            firstTeam[i].SetPosition(new Vector3(-(3*i+4), 0, 0));
        }
        for(int i = 0; i < secondTeam.Length; i++)
        {
            secondTeam[i].SetPosition(new Vector3(3*i+2, 0, 0));
        }
    }

    private void StartPlayerTurn(){
        CombatEventDic[CombatEvent.StartTurn]?.Invoke();
        // wait for player input or AI decision

    }

    private void EndPlayerTurn(){
        CombatEventDic[CombatEvent.EndTurn]?.Invoke();
        CurrentTurn += 1;
    }

    private void StartEnemyTurn(){
        CombatEventDic[CombatEvent.StartTurn]?.Invoke();
        // wait for AI decision

    }

    private void EndEnemyTurn(){
        CombatEventDic[CombatEvent.EndTurn]?.Invoke();
        CurrentTurn += 1;
    }

    private void CheckForEndCombat()
    {
        bool firstTeamAlive = false;
        foreach(var unit in firstTeam){
            if(unit.Attribute.HP > 0){
                firstTeamAlive = true;
                break;
            }
        }
        bool secondTeamAlive = false;
        foreach(var unit in secondTeam){
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
