using UnityEngine;

[CreateAssetMenu(menuName = "Combat/CombatAttribute")]
public class CombatAttribute : ScriptableObject
{
    public GameObject tempCharacterModel;
    public int MaxHPBase = 0;
    [HideInInspector] public int MaxHP =0;
    [HideInInspector] public int HP =0;
    [HideInInspector] public int Block=0;
    [HideInInspector] public int Energy=0;
    [HideInInspector] public int MaxEnergy=0;
    // Effect Attribute
    [HideInInspector] public int Strength =0;
    [HideInInspector] public int Dexterity =0;
    // Effect Attribute percent
    [HideInInspector] public float DamagePercent =0;
    [HideInInspector] public float BlockPercent =0;
    public CombatAttribute MakeCopy()
    {
        Debug.Log("Copying CombatAttribute");
        CombatAttribute characterAttribute = ScriptableObject.CreateInstance<CombatAttribute>();
        characterAttribute.MaxHPBase = this.MaxHPBase;
        characterAttribute.MaxHP = this.MaxHPBase;
        characterAttribute.HP = this.HP;
        characterAttribute.Block = this.Block;
        characterAttribute.Energy = this.Energy;
        characterAttribute.MaxEnergy = this.MaxEnergy;
        characterAttribute.Strength = this.Strength;
        characterAttribute.Dexterity = this.Dexterity;
        characterAttribute.DamagePercent = this.DamagePercent;
        characterAttribute.BlockPercent = this.BlockPercent;
        characterAttribute.tempCharacterModel = this.tempCharacterModel;
        return characterAttribute;
    }
}
