using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Conditions/TurnCount")]
public class TurnCountConditionSO : RelicConditionSO
{
    public int Interval; // 1 = every turn, 2 = every 2nd turn...
    
    public override bool Check(GameEvent gameEvent, RelicInstance relicInstance)
    {
        if (gameEvent is TurnStartEvent turnEvent)
        {
            return turnEvent.TurnNumber % Interval == 0;
        }
        return false;
    }
}
