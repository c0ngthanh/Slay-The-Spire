using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Conditions/CardTypePlayed")]
public class CardTypePlayedConditionSO : RelicConditionSO
{
    public CardType RequiredType; // Or use data.IntValue if casting to enum, but typically safer to have field if not generic

    public override bool Check(GameEvent gameEvent, RelicInstance relicInstance, RelicConditionData data)
    {
        // Assuming we have a CardPlayedEvent
        /* 
        if (gameEvent is CardPlayedEvent cardEvent)
        {
             // If we want to use data.IntValue to specify type dynamically:
             // return (int)cardEvent.Card.CardType == data.IntValue;
             
             // Or if we use the field:
             return cardEvent.Card.CardType == RequiredType;
        }
        */

        // Mock implementation since CardPlayedEvent might not exist yet
        return false;
    }
}
