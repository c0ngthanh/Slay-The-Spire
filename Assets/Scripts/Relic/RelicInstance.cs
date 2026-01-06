using System;
using System.Collections.Generic;
using UnityEngine;

public class RelicInstance
{
    public RelicSO Data { get; private set; }
    public int Counter { get; set; }
    public bool IsActive { get; set; } = true;

    // Keep track of listeners to remove them if needed (e.g. on remove relic)
    private List<Action> cleanupActions = new();

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

    private void RegisterAbility(RelicAbilityData ability)
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

    private void RegisterListener<T>(RelicAbilityData ability) where T : GameEvent
    {
        Action<T> listener = (e) =>
        {
            // Check Conditions
            if (ability.Conditions != null)
            {
                foreach (var conditionData in ability.Conditions)
                {
                    if (conditionData.Condition != null && !conditionData.Condition.Check(e, this, conditionData))
                    {
                        return;
                    }
                }
            }

            // Execute Effects
            if (ability.Effects != null)
            {
                foreach (var effectData in ability.Effects)
                {
                    if (effectData.Effect != null)
                    {
                        effectData.Effect.Execute(e, this, effectData);
                    }
                }
            }
        };
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
