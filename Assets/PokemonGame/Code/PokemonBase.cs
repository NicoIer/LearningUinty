using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PokemonGame
{
    public enum ItemEnum
    {
        None,
        奇迹种子,
    }

    /// <summary>
    /// 道具
    /// </summary>
    public class Item
    {
        #region STATIC

        public static Item find_item(ItemEnum itemEnum)
        {
            return new Item(itemEnum.ToString(), itemEnum);
        }

        #endregion

        Item(string name, ItemEnum itemEnum)
        {
            this.name = name;
            this.item_enum = itemEnum;
        }

        //ToDo 添加道具的其他效果
        public string name;
        public ItemEnum item_enum;
    }

    /// <summary>
    /// 底层的宝可梦类 用于存储相关的数据
    /// </summary>
    public class PokemonBase
    {
        #region STATIC

        public static PokemonBase create_pokemon(
            string name, string otherName = null, uint id = 0,
            CharacterEnum characterEnum = CharacterEnum.Hardy,
            Property firstProperty = Property.None, Property secondProperty = Property.None,
            Dictionary<Ability, uint> abilityValue = null,
            Dictionary<Ability, uint> individualValue = null,
            Dictionary<Ability, uint> effortValue = null,
            Dictionary<Ability, uint> raceValue = null,
            uint level = 1,
            Sex sex = Sex.None,
            ItemEnum item = ItemEnum.None,
            uint? currentHealth = null
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
                firstProperty,
                secondProperty,
                abilityValue,
                effortValue,
                raceValue,
                individualValue,
                level,
                sex,
                item,
                currentHealth);
        }

        #endregion

        public string name;
        public string otherName;
        public uint id;
        public Character character;
        public Property firstProperty;
        public Property secondProperty;
        public State state;
        public uint current_health;
        public Dictionary<Ability, uint> ability;
        public Dictionary<Ability, uint> effort;
        public Dictionary<Ability, uint> race;
        public Dictionary<Ability, uint> individual;
        public uint level;
        public Sex sex;
        public Item item;

        private PokemonBase(string name, string otherName, uint id, CharacterEnum character,
            Property firstProperty, Property secondProperty,
            Dictionary<Ability, uint> ability, Dictionary<Ability, uint> effort,
            Dictionary<Ability, uint> race, Dictionary<Ability, uint> individual,
            uint level = 1, Sex sex = Sex.None, ItemEnum item = ItemEnum.None,
            uint? currentHealth = null
        )
        {
            this.name = name;
            this.otherName = otherName;
            this.id = id;
            this.character = Character.get_character(character);
            this.state = State.None;
            this.item = Item.find_item(item);
            this.sex = sex;
            this.ability = ability;
            this.effort = effort;
            this.race = race;
            this.individual = individual;
            this.firstProperty = firstProperty;
            this.secondProperty = secondProperty;
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