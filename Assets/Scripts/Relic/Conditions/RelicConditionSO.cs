using UnityEngine;

public abstract class RelicConditionSO : ScriptableObject
{
    /// <summary>
    /// Checks if the condition is met.
    /// </summary>
    /// <param name="gameEvent">The event that triggered this check.</param>
    /// <param name="relicInstance">The runtime instance of the relic (for accessing counters, etc.).</param>
    /// <param name="data">The data associated with this condition (parameters).</param>
    /// <returns>True if the condition is met.</returns>
    public abstract bool Check(GameEvent gameEvent, RelicInstance relicInstance, RelicConditionData data);
}
