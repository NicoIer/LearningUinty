using System;
using System.Collections.Generic;
using Games.CricketGame.Code.Pokemon.Skill;
using UnityEngine;
using UnityEngine.Serialization;

namespace Games.CricketGame.Code.Pokemon.Test.ScriptableObject
{



    [CreateAssetMenu(fileName = "PokemonMeta", menuName = "Data/CricketGame/Pokemon/PokemonMeta", order = 0)]
    public class PokemonMetaInspector : UnityEngine.ScriptableObject
    {
        public List<SkillInspector> skillInspectors = new();
        public PokemonDataMeta meta;

        public void Initialize()
        {
            var skills = new Dictionary<SkillEnum, Skill.Skill>();

            foreach (var inspector in skillInspectors)
            {
                var skillMeta = SkillMeta.Find(inspector.skillEnum);//通过枚举查找技能meta信息
                var skill = new Skill.Skill(skillMeta.skillEnum, inspector.needLevel, inspector.useTimes);//通过枚举和指定信息创建skill
                skills.Add(skillMeta.skillEnum,skill);
            }

            meta.SkillMetas = skills;
            
            PokemonDataMeta.Add(meta);
            
        }
    }
}