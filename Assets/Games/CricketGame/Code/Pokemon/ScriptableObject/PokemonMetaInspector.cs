using System.Collections.Generic;
using Games.CricketGame.Code.Pokemon.Enum;
using Games.CricketGame.Code.Pokemon.Skill;
using Newtonsoft.Json;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon.Asset
{
    [CreateAssetMenu(fileName = "PokemonMetaData", menuName = "Data/CricketGame/Pokemon/PokemonMetaData", order = 0)]
    public class PokemonMetaInspector : ScriptableObject
    {
        [JsonIgnore]public bool autoCreate;
        public List<SkillInspector> skillInspectors = new();
        public PokemonDataMeta meta;

        public void OnValidate()
        {
            if(!autoCreate)
                return;
            
            var skills = new Dictionary<SkillEnum, Skill.Skill>();

            foreach (var inspector in skillInspectors)
            {
                var skillMeta = SkillMeta.Find(inspector.skillEnum); //通过枚举查找技能meta信息
                if (skillMeta.skillEnum == SkillEnum.None)
                {
                    Debug.LogWarning("对应SkillMeta没有创建.....请先创建");
                }
                var skill = new Skill.Skill(skillMeta.skillEnum, inspector.propertyEnum, inspector.needLevel,
                    inspector.useTimes); //通过枚举和指定信息创建skill
                if (!skills.ContainsKey(skillMeta.skillEnum))
                {
                    skills.Add(skillMeta.skillEnum, skill);
                }
                else
                {
                    skills[skillMeta.skillEnum] = skill;
                }

            }

            meta.SkillMetas = skills;

            PokemonDataMeta.Add(meta, true);
            PokemonDataMeta.Save();
        }
        
    }
}