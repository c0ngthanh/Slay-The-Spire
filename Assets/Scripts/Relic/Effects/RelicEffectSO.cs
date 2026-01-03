using UnityEngine;

public abstract class RelicEffectSO : ScriptableObject
{
    /// <summary>
    /// Executes the effect.
    /// </summary>
    /// <param name="gameEvent">The event that triggered this effect.</param>
    /// <param name="relicInstance">The runtime instance of the relic.</param>
    public abstract void Execute(GameEvent gameEvent, RelicInstance relicInstance);
}
