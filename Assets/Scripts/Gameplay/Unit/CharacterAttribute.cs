// using UnityEngine;

// [CreateAssetMenu(menuName = "Combat/CharacterAttribute")]
// public class CharacterAttribute : CombatAttribute
// {
//     public int MaxHPBase = 0;
//     public override int MaxHP => MaxHPBase;

//     public override CombatAttribute CopyAttribute(CombatAttribute combatAttribute)
//     {
//         Debug.Log("Copying CharacterAttribute");
//         CharacterAttribute characterAttribute = ScriptableObject.CreateInstance<CharacterAttribute>();
//         characterAttribute.MaxHPBase = this.MaxHPBase;
//         characterAttribute.HP = this.HP;
//         characterAttribute.Block = this.Block;
//         characterAttribute.Energy = this.Energy;
//         characterAttribute.MaxEnergy = this.MaxEnergy;
//         characterAttribute.Strength = this.Strength;
//         characterAttribute.Dexterity = this.Dexterity;
//         characterAttribute.DamagePercent = this.DamagePercent;
//         characterAttribute.BlockPercent = this.BlockPercent;
//         return characterAttribute;
//     }
// }
