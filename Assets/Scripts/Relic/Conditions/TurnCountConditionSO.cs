using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Conditions/TurnCount")]
public class TurnCountConditionSO : RelicConditionSO
{
    public override bool Check(GameEvent gameEvent, RelicInstance relicInstance, RelicConditionData data)
    {
        if (gameEvent is TurnStartEvent turnEvent)
        {
            return turnEvent.TurnNumber % data.IntValue == 0;
        }
        return false;
    }
}
