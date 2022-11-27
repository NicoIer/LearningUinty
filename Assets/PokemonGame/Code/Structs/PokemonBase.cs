using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonGame.Structs;
using UnityEngine;

namespace PokemonGame.Code.Structs
{
    public enum Sex
    {
        None,
        Man,
        Woman,
    }

    public enum PokemonEnum
    {
        妙蛙种子,
        妙蛙花,
        小火龙,
    }

    /// <summary>
    /// 底层的宝可梦类 用于存储相关的数据
    /// </summary>
    public class PokemonBase
    {
        #region STATIC

        #region Search

        public static Pokemon find_pokemon(uint pokemonID)
        {
            //ToDo 完成这里的工作
            return null;
        }

        public static Pokemon find_pokemon(string pokemonName)
        {
            //ToDo 完成这里的工作
            return null;
        }

        public static PokemonBase find_pokemon(PokemonEnum pokemonEnum)
        {
            //ToDo 完成这里的工作
            return null;
        }

        #endregion

        #region Create

        public static PokemonBase create_pokemon(
            string name, string otherName = null, uint id = 0,
            CharacterEnum characterEnum = CharacterEnum.Hardy,
            PropertyEnum firstPropertyEnum = PropertyEnum.无属性, PropertyEnum secondPropertyEnum = PropertyEnum.无属性,
            Dictionary<Ability, uint> abilityValue = null,
            Dictionary<Ability, uint> individualValue = null,
            Dictionary<Ability, uint> effortValue = null,
            Dictionary<Ability, uint> raceValue = null,
            uint level = 1,
            Sex sex = Sex.None,
            ItemEnum item = ItemEnum.None,
            uint? currentHealth = null,
            uint expNow = 0,
            uint expNeed = 0,
            SkillEnum skillEnum1 = SkillEnum.None,
            SkillEnum skillEnum2 = SkillEnum.None,
            SkillEnum skillEnum3 = SkillEnum.None,
            SkillEnum skillEnum4 = SkillEnum.None,
            PeculiarityEnum peculiarityEnum = PeculiarityEnum.None,
            Trainer trainer = null,
            StateEnum stateEnum = StateEnum.None
        )
        {
            abilityValue ??= new Dictionary<Ability, uint>();

            individualValue ??= new Dictionary<Ability, uint>();

            effortValue ??= new Dictionary<Ability, uint>();

            raceValue ??= new Dictionary<Ability, uint>();

            foreach (Ability ability in Enum.GetValues(typeof(Ability)))
            {
                if (!abilityValue.ContainsKey(ability))
                {
                    abilityValue.Add(ability, 0);
                }

                if (!individualValue.ContainsKey(ability))
                {
                    individualValue.Add(ability, 0);
                }

                if (!effortValue.ContainsKey(ability))
                {
                    effortValue.Add(ability, 0);
                }

                if (!raceValue.ContainsKey(ability))
                {
                    raceValue.Add(ability, 0);
                }
            }

            otherName ??= name;

            return new PokemonBase(
                name,
                otherName,
                id,
                characterEnum,
                firstPropertyEnum,
                secondPropertyEnum,
                abilityValue,
                effortValue,
                raceValue,
                individualValue,
                level,
                sex,
                item,
                currentHealth,
                expNow,
                expNeed,
                skillEnum1,
                skillEnum2,
                skillEnum3,
                skillEnum4,
                peculiarityEnum,
                trainer,
                stateEnum
            );
        }

        #endregion

        #region Attribute

        #endregion
        #endregion

        #region Attribute

        public readonly string uid;
        public readonly string name;
        public readonly string otherName;
        public readonly uint id;
        public readonly Character character;
        public readonly Property firstProperty;
        public readonly Property secondProperty;
        public readonly State state;
        public readonly uint current_health;
        public Dictionary<Ability, uint> ability;
        public Dictionary<Ability, uint> effort;
        public Dictionary<Ability, uint> race;
        public Dictionary<Ability, uint> individual;
        public readonly uint level;
        public readonly Sex sex;
        public Item item;
        public uint exp_now;
        public uint exp_need;

        public Skill skill1;
        public Skill skill2;
        public Skill skill3;
        public Skill skill4;
        public Peculiarity peculiarity;
        public bool isEgg = false;
        public Trainer trainer;

        #endregion

        private PokemonBase(string name,
            string otherName,
            uint id,
            CharacterEnum character,
            PropertyEnum firstPropertyEnum,
            PropertyEnum secondPropertyEnum,
            Dictionary<Ability, uint> ability,
            Dictionary<Ability, uint> effort,
            Dictionary<Ability, uint> race,
            Dictionary<Ability, uint> individual,
            uint level = 1,
            Sex sex = Sex.None,
            ItemEnum item = ItemEnum.None,
            uint? currentHealth = null,
            uint expNow = 0,
            uint expNeed = 0,
            SkillEnum skillEnum1 = SkillEnum.None,
            SkillEnum skillEnum2 = SkillEnum.None,
            SkillEnum skillEnum3 = SkillEnum.None,
            SkillEnum skillEnum4 = SkillEnum.None,
            PeculiarityEnum peculiarityEnum = PeculiarityEnum.None,
            Trainer trainer = null,
            StateEnum stateEnum = StateEnum.None
        )
        {
            this.name = name;
            this.otherName = otherName;
            this.id = id;
            this.character = Character.find_character(character);
            state = State.find_state(stateEnum);
            this.item = Item.find_item(item);
            this.sex = sex;
            this.ability = ability;
            this.effort = effort;
            this.race = race;
            this.individual = individual;
            firstProperty = Property.find_property(firstPropertyEnum);
            secondProperty = Property.find_property(secondPropertyEnum);
            //确保所有的能力都有其value
            foreach (Ability a in Enum.GetValues(typeof(Ability)))
            {
                if (!this.ability.ContainsKey(a))
                {
                    this.ability.Add(a, 0);
                }

                if (!this.effort.ContainsKey(a))
                {
                    this.effort.Add(a, 0);
                }

                if (!this.race.ContainsKey(a))
                {
                    this.race.Add(a, 0);
                }

                if (!this.individual.ContainsKey(a))
                {
                    this.individual.Add(a, 0);
                }
            }

            this.level = level;
            update_ability();
            if (currentHealth is null or 0)
            {
                current_health = ability[Ability.Health]; //计算能力值后 再 给当前血量赋初始值
            }
            else
            {
                current_health = (uint)currentHealth;
            }

            //经验
            exp_now = expNow;
            exp_need = expNeed;

            //初始技能
            if (skillEnum1 == SkillEnum.None)
            {
                Debug.LogWarning("这只宝可梦什么技能也不会!!");
            }

            skill1 = Skill.find_skill(skillEnum1);
            skill2 = Skill.find_skill(skillEnum2);
            skill3 = Skill.find_skill(skillEnum3);
            skill4 = Skill.find_skill(skillEnum4);
            //特效
            if (peculiarityEnum == PeculiarityEnum.None)
            {
                Debug.LogWarning("这只Pokemon没有特效");
            }

            peculiarity = Peculiarity.FindPeculiarity(peculiarityEnum);

            //
            this.trainer = trainer;
            uid = Guid.NewGuid().ToString("N");
        }

        public void update_ability()
        {
            check_effort();

            var rate = level / 100.0;
            foreach (Ability a in Enum.GetValues(typeof(Ability)))
            {
                if (a != Ability.Health)
                {
                    ability[a] = (ushort)((race[a] * 2 +
                                           individual[a] +
                                           effort[a] / 4) * rate + 5);
                }
                else
                {
                    ability[a] =
                        (ushort)((race[a] * 2 + individual[a] +
                                  effort[a] / 4) * rate + 10 + level);
                }
            }

            if (!character.none)
            {
                ability[character.easy] = (ushort)(ability[character.easy] * 1.1);
                ability[character.hard] = (ushort)(ability[character.easy] * 0.9);
            }
        }

        private void check_effort()
        {
            var total = effort.Values.Sum(x => x);
            if (total > 510)
            {
                foreach (Ability a in Enum.GetValues(typeof(Ability)))
                {
                    effort[a] = 0;
                }
            }
            else
            {
                foreach (Ability a in Enum.GetValues(typeof(Ability)))
                {
                    effort[a] = (uint)Mathf.Clamp(effort[a], 0, 255);
                }
            }
        }

        private void gain_effort(Ability ability, uint point)
        {
            effort[ability] = (uint)Mathf.Clamp(effort[ability] + point, 0, 255);
        }
    }
}