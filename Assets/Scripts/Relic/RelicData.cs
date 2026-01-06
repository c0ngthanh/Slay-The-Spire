using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRelic", menuName = "Relic/RelicSO")]
public class RelicSO : ScriptableObject
{
    public string relicName;
    public Sprite icon;
    public RarityType rarity;
    public CharacterClass characterClass;
    [TextArea] public string flavorText;
    [TextArea] public string description;

    [Header("Behavior")]
    public List<RelicAbilityData> abilities;
    public List<RelicPassiveData> passives;
}

[System.Serializable]
public class RelicAbilityData
{
    public GameTriggerType Trigger;
    public List<RelicConditionData> Conditions;
    public List<RelicEffectData> Effects;
}

[System.Serializable]
public class RelicConditionData
{
    public RelicConditionSO Condition;
    public int IntValue;
    public float FloatValue;
    public bool BoolValue;
}

[System.Serializable]
public class RelicEffectData
{
    public RelicEffectSO Effect;
    public int IntValue;
    public float FloatValue;
    public string StringValue;
}

[System.Serializable]
public class RelicPassiveData
{
    public StatType Stat;
    public float ModifierValue; // e.g. 0.5 for 50%, or 10 for +10
    public ModifierType Type;
}

public enum ModifierType
{
    Add, // Base + Value
    Multiply, // Base * Value
    Override // Set to Value
}
