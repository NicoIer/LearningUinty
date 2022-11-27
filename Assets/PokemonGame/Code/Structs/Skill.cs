﻿using System;

namespace PokemonGame
{
    public enum SkillEnum
    {
        None,
        电光一闪,
    }

    public enum SkillTypeEnum
    {
        物理,
        变化,
        特殊
    }

    public class Skill
    {
        #region Static

        public static Skill find_skill(SkillEnum skillEnum)
        {//ToDo 实现代码和数据分离
            switch (skillEnum)
            {
                case SkillEnum.None:
                    return null;
                case SkillEnum.电光一闪://ToDo 不确定Order应该是如何的
                    return new Skill(
                        skillEnum,
                        "电光一闪",
                        PropertyEnum.Genreal,
                        30,
                        "以迅雷不及掩耳之势扑击对手,必定能够先制攻击",
                        SkillTypeEnum.物理,
                        40,
                        1.0f,
                        0
                    );
                default:
                    throw new ArgumentOutOfRangeException(nameof(skillEnum), skillEnum, null);
            }
        }

        public static Skill find_skill(string skill_name)
        {
            return null;
        }
        public static Skill find_skill(uint skill_id)
        {
            return null;
        }

        #endregion

        

        public SkillEnum skillEnum;
        public string name;
        public PropertyEnum propertyEnum;
        public uint pp;
        public string desc;
        public SkillTypeEnum typeEnum;
        public uint power;
        public float hit_rate;

        public byte order;
        //ToDo 应该还有一个动画效果
        public Skill(SkillEnum skillEnum,string name, PropertyEnum propertyEnum, uint pp, string desc, SkillTypeEnum typeEnum, uint power, float hitRate, byte order)
        {
            this.skillEnum = skillEnum;
            this.name = name;
            this.propertyEnum = propertyEnum;
            this.pp = pp;
            this.desc = desc;
            this.typeEnum = typeEnum;
            this.power = power;
            hit_rate = hitRate;
            this.order = order;
        }
    }
}