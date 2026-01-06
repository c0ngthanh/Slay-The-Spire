using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Conditions/HPThreshold")]
public class HPThresholdConditionSO : RelicConditionSO
{
    public override bool Check(GameEvent gameEvent, RelicInstance relicInstance, RelicConditionData data)
    {
        // Mock implementation
        float currentHPPercent = 0.4f; // Mocked value
                                       // float currentHPPercent = Player.Instance.CurrentHP / Player.Instance.MaxHP;

        if (data.BoolValue) // IsBelow
            return currentHPPercent <= data.FloatValue;
        else
            return currentHPPercent >= data.FloatValue;
    }
}
