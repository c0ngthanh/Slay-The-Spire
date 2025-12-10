using UnityEngine;

[CreateAssetMenu(menuName = "Combat/CharacterAttribute")]
public class CharacterAttribute : CombatAttribute
{
    public int MaxHPBase = 0;
    public override int MaxHP => MaxHPBase;
}
