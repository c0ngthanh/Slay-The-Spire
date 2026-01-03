using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Ability")]
public class RelicAbilitySO : ScriptableObject
{
    public GameTriggerType Trigger;

    [Tooltip("All conditions must be met for the effects to run.")]
    public List<RelicConditionSO> Conditions = new List<RelicConditionSO>();

    public List<RelicEffectSO> Effects = new List<RelicEffectSO>();

    public void Execute(GameEvent gameEvent, RelicInstance instance)
    {
        // Check Conditions
        foreach (var condition in Conditions)
        {
            if (!condition.Check(gameEvent, instance))
            {
                return;
            }
        }

        // Execute Effects
        foreach (var effect in Effects)
        {
            effect.Execute(gameEvent, instance);
        }
    }
}
