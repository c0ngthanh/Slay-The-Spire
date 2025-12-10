using UnityEngine;

public class CombatAttribute : ScriptableObject
{
    public virtual int MaxHP => 0;
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
}
