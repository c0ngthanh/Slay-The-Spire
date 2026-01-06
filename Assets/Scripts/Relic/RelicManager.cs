using UnityEngine;
using System.Collections.Generic;

public class RelicManager : MonoBehaviour
{
    public List<RelicInstance> ownedRelics = new();

    // For testing/reference
    public List<RelicSO> startingRelics;

    private void Start()
    {
        foreach (var relicSO in startingRelics)
        {
            AddRelic(relicSO);
        }
    }

    public void AddRelic(RelicSO relicData)
    {
        if (relicData == null) return;

        RelicInstance newInstance = new(relicData);
        ownedRelics.Add(newInstance);

        Debug.Log($"Relic Added: {relicData.relicName}");

        // Handle "Upon pickup" effects immediately if any (optional, depends on design)
        // If we had an OnObtain relic trigger, we'd fire an event here.
    }

    public void RemoveRelic(RelicInstance relic)
    {
        if (ownedRelics.Contains(relic))
        {
            relic.OnRemove();
            ownedRelics.Remove(relic);
        }
    }

    /// <summary>
    /// Calculates the final value of a stat by applying all relic modifiers.
    /// </summary>
    /// <param name="stat">The stat to calculate.</param>
    /// <param name="baseValue">The base value to start with (default 0 for add/subtract, 1 for multipliers).</param>
    /// <returns>The modified value.</returns>
    public float GetStatValue(StatType stat, float baseValue)
    {
        float finalValue = baseValue;

        // Apply "Add" modifiers first
        foreach (var relic in ownedRelics)
        {
            if (!relic.IsActive || relic.Data.passives == null) continue;

            foreach (var passive in relic.Data.passives)
            {
                if (passive.Stat == stat && passive.Type == ModifierType.Add)
                {
                    finalValue += passive.ModifierValue;
                }
            }
        }

        // Apply "Multiply" modifiers
        foreach (var relic in ownedRelics)
        {
            if (!relic.IsActive || relic.Data.passives == null) continue;

            foreach (var passive in relic.Data.passives)
            {
                if (passive.Stat == stat && passive.Type == ModifierType.Multiply)
                {
                    finalValue *= passive.ModifierValue;
                }
            }
        }

        // Apply "Override" modifiers (last one wins)
        foreach (var relic in ownedRelics)
        {
            if (!relic.IsActive || relic.Data.passives == null) continue;

            foreach (var passive in relic.Data.passives)
            {
                if (passive.Stat == stat && passive.Type == ModifierType.Override)
                {
                    finalValue = passive.ModifierValue;
                }
            }
        }

        return finalValue;
    }
}
