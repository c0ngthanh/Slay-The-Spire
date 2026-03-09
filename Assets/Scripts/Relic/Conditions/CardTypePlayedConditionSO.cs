using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Conditions/CardTypePlayed")]
public class CardTypePlayedConditionSO : RelicConditionSO
{
    public CardType RequiredType; // Or use data.IntValue if casting to enum, but typically safer to have field if not generic

    public override bool Check(GameEvent gameEvent, RelicRuntime relicInstance, RelicConditionData data)
    {
        if (gameEvent is CardPlayedEvent cardEvent)
        {
            return cardEvent.CardSO.CardType == RequiredType;
        }

        return false;
    }
}
