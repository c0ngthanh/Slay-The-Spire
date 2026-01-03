using System;
using System.Collections.Generic;
using UnityEngine;

public class RelicInstance
{
    public RelicSO Data { get; private set; }
    public int Counter { get; set; }
    public bool IsActive { get; set; } = true;

    // Keep track of listeners to remove them if needed (e.g. on remove relic)
    private List<Action> cleanupActions = new List<Action>();

    public RelicInstance(RelicSO data)
    {
        Data = data;
        RegisterAbilities();
    }

    private void RegisterAbilities()
    {
        if (Data.abilities == null) return;

        foreach (var ability in Data.abilities)
        {
            RegisterAbility(ability);
        }
    }

    private void RegisterAbility(RelicAbilitySO ability)
    {
        switch (ability.Trigger)
        {
            case GameTriggerType.OnCombatEnd:
                RegisterListener<CombatEndEvent>(ability);
                break;
            case GameTriggerType.OnCombatStart:
                RegisterListener<CombatStartEvent>(ability);
                break;
            case GameTriggerType.OnTurnStart:
                RegisterListener<TurnStartEvent>(ability);
                break;
            case GameTriggerType.OnTurnNumber:
                RegisterListener<TurnStartEvent>(ability); // Conditions will check number
                break;
             case GameTriggerType.OnLoseHP:
                RegisterListener<PlayerLoseHPEvent>(ability);
                break;
            // Add other cases as needed...
            default:
                Debug.LogWarning($"RelicTrigger {ability.Trigger} not registered in RelicInstance.");
                break;
        }
    }

    private void RegisterListener<T>(RelicAbilitySO ability) where T : GameEvent
    {
        Action<T> listener = (e) => ability.Execute(e, this);
        GlobalEventManager.Instance.AddListener(listener);
        cleanupActions.Add(() => GlobalEventManager.Instance.RemoveListener(listener));
    }

    public void OnRemove()
    {
        foreach (var action in cleanupActions)
        {
            action.Invoke();
        }
        cleanupActions.Clear();
    }
}
