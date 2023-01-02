using System.Collections.Generic;
using Games.CricketGame.Code.Pokemon.Enum;
using Games.CricketGame.Code.Pokemon.Skill;
using UnityEngine;
using UnityEngine.Serialization;

namespace Games.CricketGame.Code.Pokemon.Asset
{
    [CreateAssetMenu(fileName = "PokemonMetaData", menuName = "Data/CricketGame/Pokemon/PokemonMetaData", order = 0)]
    public class PokemonMetaInspector : ScriptableObject
    {
        public bool autoReplace;
        public List<SkillInspector> skillInspectors = new();
        public PokemonDataMeta meta;

        public void OnValidate()
        {
            PokemonDataMeta dataMeta = null;
            try
            {
                dataMeta = PokemonDataMeta.Find(meta.pokemonEnum);
            }
            catch (KeyNotFoundException)
            {
            }

            if (dataMeta != null)
            {
                if (autoReplace)
                {
                    Debug.LogWarning("对应精灵Meta数据已经创建,正在进行覆盖");
                }
                else
                {
                    return;
                }
            }

            var skills = new Dictionary<SkillEnum, Skill.Skill>();

            foreach (var inspector in skillInspectors)
            {
                var skillMeta = SkillMeta.Find(inspector.skillEnum); //通过枚举查找技能meta信息
                var skill = new Skill.Skill(skillMeta.skillEnum, inspector.needLevel,
                    inspector.useTimes); //通过枚举和指定信息创建skill
                skills.Add(skillMeta.skillEnum, skill);
            }

            meta.SkillMetas = skills;

            PokemonDataMeta.Add(meta, true);
            PokemonDataMeta.Save();
        }
    }
}