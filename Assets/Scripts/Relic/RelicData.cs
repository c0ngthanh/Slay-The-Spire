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
    public List<RelicAbilitySO> abilities;
}
