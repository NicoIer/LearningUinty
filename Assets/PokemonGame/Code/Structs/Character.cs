using System;
using PokemonGame.Structs;

namespace PokemonGame
{    public enum CharacterEnum
    {
        Hardy,
        Lonely,
        Adamant,
        Naughty,
        Brave,
        Bold,
        Docile,
        Impish,
        Lax,
        Relaxed,
        Modest,
        Mild,
        Bashful,
        Rash,
        Quiet,
        Calm,
        Gentle,
        Careful,
        Quicky,
        Sassy,
        Timid,
        Hasty,
        Jolly,
        Naive,
        Serious
    }
    public class Character
    {
        public static Character find_character(CharacterEnum character)
        {
            var newCharacter = new Character();
            switch (character)
            {
                case CharacterEnum.Hardy:
                case CharacterEnum.Docile:
                case CharacterEnum.Bashful:
                case CharacterEnum.Quicky:
                case CharacterEnum.Serious:
                    newCharacter.none = true;
                    break;
                case CharacterEnum.Lonely:
                    newCharacter.easy = Ability.ObjectAttack;
                    newCharacter.hard = Ability.ObjectDefense;
                    break;
                case CharacterEnum.Adamant:
                    newCharacter.easy = Ability.ObjectAttack;
                    newCharacter.hard = Ability.SpecialAttack;
                    break;
                case CharacterEnum.Naughty:
                    newCharacter.easy = Ability.ObjectAttack;
                    newCharacter.hard = Ability.SpecialDefense;
                    break;
                case CharacterEnum.Brave:
                    newCharacter.easy = Ability.ObjectAttack;
                    newCharacter.hard = Ability.Speed;
                    break;
                case CharacterEnum.Bold:
                    newCharacter.easy = Ability.ObjectDefense;
                    newCharacter.hard = Ability.ObjectAttack;
                    break;
                case CharacterEnum.Impish:
                    newCharacter.easy = Ability.ObjectDefense;
                    newCharacter.hard = Ability.SpecialAttack;
                    break;
                case CharacterEnum.Lax:
                    newCharacter.easy = Ability.ObjectDefense;
                    newCharacter.hard = Ability.SpecialDefense;
                    break;
                case CharacterEnum.Relaxed:
                    newCharacter.easy = Ability.ObjectDefense;
                    newCharacter.hard = Ability.Speed;
                    break;
                case CharacterEnum.Modest:
                    newCharacter.easy = Ability.SpecialAttack;
                    newCharacter.hard = Ability.ObjectAttack;
                    break;
                case CharacterEnum.Mild:
                    newCharacter.easy = Ability.SpecialAttack;
                    newCharacter.hard = Ability.ObjectDefense;
                    break;
                case CharacterEnum.Rash:
                    newCharacter.easy = Ability.SpecialAttack;
                    newCharacter.hard = Ability.SpecialDefense;
                    break;
                case CharacterEnum.Quiet:
                    newCharacter.easy = Ability.SpecialAttack;
                    newCharacter.hard = Ability.Speed;
                    break;
                case CharacterEnum.Calm:
                    newCharacter.easy = Ability.SpecialDefense;
                    newCharacter.hard = Ability.ObjectAttack;
                    break;
                case CharacterEnum.Gentle:
                    newCharacter.easy = Ability.SpecialDefense;
                    newCharacter.hard = Ability.ObjectDefense;
                    break;
                case CharacterEnum.Careful:
                    newCharacter.easy = Ability.SpecialDefense;
                    newCharacter.hard = Ability.SpecialAttack;
                    break;
                case CharacterEnum.Sassy:
                    newCharacter.easy = Ability.SpecialDefense;
                    newCharacter.hard = Ability.Speed;
                    break;
                case CharacterEnum.Timid:
                    newCharacter.easy = Ability.Speed;
                    newCharacter.hard = Ability.ObjectAttack;
                    break;
                case CharacterEnum.Hasty:
                    newCharacter.easy = Ability.Speed;
                    newCharacter.hard = Ability.ObjectDefense;
                    break;
                case CharacterEnum.Jolly:
                    newCharacter.easy = Ability.Speed;
                    newCharacter.hard = Ability.SpecialAttack;
                    break;
                case CharacterEnum.Naive:
                    newCharacter.easy = Ability.Speed;
                    newCharacter.hard = Ability.SpecialDefense;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return newCharacter;
        }

        public CharacterEnum characterEnum; //性格
        public bool none; //是否无修正
        public Ability easy; //容易提升的

        public Ability hard; //不容易提升
        // 口味
    }
}