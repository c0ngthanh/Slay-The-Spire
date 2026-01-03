using UnityEngine;

[CreateAssetMenu(menuName = "Relic/Conditions/HPThreshold")]
public class HPThresholdConditionSO : RelicConditionSO
{
    public float Percentage; // 0.5 for 50%
    public bool IsBelow; // True for "<=", False for ">="

    public override bool Check(GameEvent gameEvent, RelicInstance relicInstance)
    {
        // Mock implementation
        float currentHPPercent = 0.4f; // Mocked value
        // float currentHPPercent = Player.Instance.CurrentHP / Player.Instance.MaxHP;
        
        if (IsBelow)
            return currentHPPercent <= Percentage;
        else
            return currentHPPercent >= Percentage;
    }
}
