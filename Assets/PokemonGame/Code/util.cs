using System.Collections.Generic;

namespace PokemonGame
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct AbilityValue
    {
        [SerializeField] internal uint Health;
        [SerializeField] internal uint ObjectAttack;
        [SerializeField] internal uint SpecialAttack;
        [SerializeField] internal uint ObjectDefense;
        [SerializeField] internal uint SpecialDefense;
        [SerializeField] internal uint Speed;
    }

    [Serializable]
    public struct IndividualValue
    {
        [SerializeField] internal ushort Health;
        [SerializeField] internal ushort ObjectAttack;
        [SerializeField] internal ushort SpecialAttack;
        [SerializeField] internal ushort ObjectDefense;
        [SerializeField] internal ushort SpecialDefense;
        [SerializeField] internal ushort Speed;
    }

    [Serializable]
    public struct RaceValue
    {
        [SerializeField] internal ushort Health;
        [SerializeField] internal ushort ObjectAttack;
        [SerializeField] internal ushort SpecialAttack;
        [SerializeField] internal ushort ObjectDefense;
        [SerializeField] internal ushort SpecialDefense;
        [SerializeField] internal ushort Speed;
    }

    [Serializable]
    public struct EffortValue
    {
        [SerializeField] internal ushort Health;
        [SerializeField] internal ushort ObjectAttack;
        [SerializeField] internal ushort SpecialAttack;
        [SerializeField] internal ushort ObjectDefense;
        [SerializeField] internal ushort SpecialDefense;
        [SerializeField] internal ushort Speed;
    }

    public enum CharacterEnum
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

    public enum Sex
    {
        None,
        Man,
        Woman,
    }

    public enum Ability
    {
        Health,
        ObjectAttack,
        SpecialAttack,
        ObjectDefense,
        SpecialDefense,
        Speed,
    }

    public enum State
    {
        None,
        Poisoning,
        Sleeping
    }

    [Serializable]
    public struct Character
    {
        public static Character get_character(CharacterEnum character)
        {
            var _character = new Character();
            switch (character)
            {
                case CharacterEnum.Hardy:
                case CharacterEnum.Docile:
                case CharacterEnum.Bashful:
                case CharacterEnum.Quicky:
                case CharacterEnum.Serious:
                    _character.none = true;
                    break;
                case CharacterEnum.Lonely:
                    _character.easy = Ability.ObjectAttack;
                    _character.hard = Ability.ObjectDefense;
                    break;
                case CharacterEnum.Adamant:
                    _character.easy = Ability.ObjectAttack;
                    _character.hard = Ability.SpecialAttack;
                    break;
                case CharacterEnum.Naughty:
                    _character.easy = Ability.ObjectAttack;
                    _character.hard = Ability.SpecialDefense;
                    break;
                case CharacterEnum.Brave:
                    _character.easy = Ability.ObjectAttack;
                    _character.hard = Ability.Speed;
                    break;
                case CharacterEnum.Bold:
                    _character.easy = Ability.ObjectDefense;
                    _character.hard = Ability.ObjectAttack;
                    break;
                case CharacterEnum.Impish:
                    _character.easy = Ability.ObjectDefense;
                    _character.hard = Ability.SpecialAttack;
                    break;
                case CharacterEnum.Lax:
                    _character.easy = Ability.ObjectDefense;
                    _character.hard = Ability.SpecialDefense;
                    break;
                case CharacterEnum.Relaxed:
                    _character.easy = Ability.ObjectDefense;
                    _character.hard = Ability.Speed;
                    break;
                case CharacterEnum.Modest:
                    _character.easy = Ability.SpecialAttack;
                    _character.hard = Ability.ObjectAttack;
                    break;
                case CharacterEnum.Mild:
                    _character.easy = Ability.SpecialAttack;
                    _character.hard = Ability.ObjectDefense;
                    break;
                case CharacterEnum.Rash:
                    _character.easy = Ability.SpecialAttack;
                    _character.hard = Ability.SpecialDefense;
                    break;
                case CharacterEnum.Quiet:
                    _character.easy = Ability.SpecialAttack;
                    _character.hard = Ability.Speed;
                    break;
                case CharacterEnum.Calm:
                    _character.easy = Ability.SpecialDefense;
                    _character.hard = Ability.ObjectAttack;
                    break;
                case CharacterEnum.Gentle:
                    _character.easy = Ability.SpecialDefense;
                    _character.hard = Ability.ObjectDefense;
                    break;
                case CharacterEnum.Careful:
                    _character.easy = Ability.SpecialDefense;
                    _character.hard = Ability.SpecialAttack;
                    break;
                case CharacterEnum.Sassy:
                    _character.easy = Ability.SpecialDefense;
                    _character.hard = Ability.Speed;
                    break;
                case CharacterEnum.Timid:
                    _character.easy = Ability.Speed;
                    _character.hard = Ability.ObjectAttack;
                    break;
                case CharacterEnum.Hasty:
                    _character.easy = Ability.Speed;
                    _character.hard = Ability.ObjectDefense;
                    break;
                case CharacterEnum.Jolly:
                    _character.easy = Ability.Speed;
                    _character.hard = Ability.SpecialAttack;
                    break;
                case CharacterEnum.Naive:
                    _character.easy = Ability.Speed;
                    _character.hard = Ability.SpecialDefense;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _character;
        }

        public CharacterEnum _character; //性格
        [HideInInspector] public bool none; //是否无修正
        [HideInInspector] public Ability easy; //容易提升的

        [HideInInspector] public Ability hard; //不容易提升
        // 口味
    }

    /// <summary>
    /// PokemonProperty - 宝可梦的属性
    /// 
    /// </summary>
    [Serializable]
    public enum Property
    {
        None, //未定
        Genreal, //一般
        Combat, //格斗
        Flight, //飞行
        Poison, //毒
        Ground, //地面
        Rock, //岩石
        Worm, //虫
        Ghost, //幽灵
        Stell, //钢
        Fire, //火
        Water, //水
        Grass, //草
        Electricity, //电
        Superpowers, //超能
        Ice, //冰
        Dragon, //龙
        Evil, //恶
        Goblin, //妖精
    }

    /// <summary>
    /// 属性值
    /// </summary>
    [Serializable]
    internal class PropertyValue
    {
        [SerializeField] internal List<Property> _propertys;
    }
}