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
    private class Turn
    {
        public CombatUnit[] firstTeam;
        public CombatUnit[] secondTeam;
        public int turnOrder;

        public Turn(CombatUnit[] firstTeam,CombatUnit[] secondTeam, int turnOrder)
        {
            this.firstTeam = firstTeam;
            this.secondTeam = secondTeam;
            this.turnOrder = turnOrder;
        }
    }
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
}
