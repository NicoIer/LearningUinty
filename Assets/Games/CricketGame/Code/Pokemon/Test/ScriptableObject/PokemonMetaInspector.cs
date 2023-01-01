using System.Collections.Generic;
using Games.CricketGame.Code.Pokemon.Skill;
using UnityEngine;
using UnityEngine.Serialization;

namespace Games.CricketGame.Code.Pokemon.Test.ScriptableObject
{

    [CreateAssetMenu(fileName = "PokemonMeta", menuName = "Data/CricketGame/Pokemon/PokemonMeta", order = 0)]
    public class PokemonMetaInspector : UnityEngine.ScriptableObject
    {
        public List<Skill.Skill> skills = new();
        public PokemonDataMeta meta;

        public void Initialize()
        {
            var skillMetas = new Dictionary<SkillEnum, Skill.Skill>();

            foreach (var skill in skills)
            {
               skillMetas.Add(skill.meta.skillEnum,skill);
               SkillMeta.Add(skill.meta);
            }

            meta.SkillMetas = skillMetas;
            
            PokemonDataMeta.Add(meta);
            
        }
    }
}