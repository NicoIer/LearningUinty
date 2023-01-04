using System;
using System.Collections.Generic;
using System.IO;
using Games.CricketGame.Code.Pokemon.Enum;
using Games.CricketGame.Code.Pokemon.Skill;
using Newtonsoft.Json;
using Nico.Common;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon
{
    [CreateAssetMenu(fileName = "PokemonMetaData", menuName = "Data/CricketGame/Pokemon/PokemonMetaData", order = 0)]
    [Serializable]
    public class PokemonDataMeta : ScriptableObject
    {
        #region Static Attribute

        private static Dictionary<PokemonEnum, PokemonDataMeta> _pokemonMeta;
        private static readonly string _pokemon_meta_path = "pokemon/pokemonMeta.json";
        private static bool _initilized = false;

        #endregion

        #region Static Method

        private static void InitializeStatic()
        {
            try
            {
                _pokemonMeta =
                    JsonResourcesManager.LoadStreamingAssets<Dictionary<PokemonEnum, PokemonDataMeta>>(
                        _pokemon_meta_path);
            }
            catch (FileNotFoundException)
            {
                _pokemonMeta = new();
            }
            catch (DirectoryNotFoundException)
            {
                _pokemonMeta = new();
            }

            _initilized = true;
        }

        public static void Save()
        {
            JsonResourcesManager.SaveStreamingAssets(_pokemonMeta, _pokemon_meta_path, true);
        }

        public static void Add(PokemonDataMeta meta, bool replace)
        {
            if (!_initilized)
            {
                InitializeStatic();
            }

            if (!_pokemonMeta.ContainsKey(meta.pokemonEnum))
            {
                _pokemonMeta.Add(meta.pokemonEnum, meta);
            }
            else if (replace)
            {
                _pokemonMeta[meta.pokemonEnum] = meta;
                Debug.LogWarning($"重写了{meta.pokemonEnum}的Meta数据");
            }
            else
            {
                throw new Exception("未指定replace");
            }
        }

        public static PokemonDataMeta Find(PokemonEnum pokemonEnum)
        {
            if (!_initilized)
            {
                InitializeStatic();
            }

            if (_pokemonMeta.ContainsKey(pokemonEnum))
            {
                return _pokemonMeta[pokemonEnum];
            }

            throw new KeyNotFoundException($"{pokemonEnum}未存放到json文件");
        }

        #endregion

        #region unity 面板信息

        [JsonIgnore] public bool autoCreate;
        [JsonIgnore] public List<SkillInspector> skillInspectors = new();

        #endregion

        #region 数据记录信息

        public PropertyEnum property;
        public PokemonEnum pokemonEnum;
        public ExperienceEnum experienceEnum;
        public int healthRace;
        public int attackRace;
        public int defenseRace;
        public int specialAttackRace;
        public int specialDefenseRace;
        public int speedRace;
        public string desc; //图鉴描述
        public Dictionary<SkillEnum, Skill.Skill> skillMetas;
        public PokemonEnum previousLevel;
        public List<PokemonEnum> nextLevel = new();

        #endregion

        #region unity自带SO信息


        public void OnValidate()
        {
            if (!autoCreate)
                return;


            skillMetas = new Dictionary<SkillEnum, Skill.Skill>();
            foreach (var inspector in skillInspectors)
            {
                var skillMeta = SkillMeta.Find(inspector.skillEnum); //通过枚举查找技能meta信息
                if (skillMeta.skillEnum == SkillEnum.None)
                {
                    Debug.LogWarning("对应SkillMeta没有创建.....请先创建");
                }

                var skill = new Skill.Skill(skillMeta.skillEnum, inspector.propertyEnum, inspector.needLevel,
                    inspector.useTimes); //通过枚举和指定信息创建skill
                if (!skillMetas.ContainsKey(skillMeta.skillEnum))
                {
                    skillMetas.Add(skillMeta.skillEnum, skill);
                }
                else
                {
                    skillMetas[skillMeta.skillEnum] = skill;
                }
            }

            Add(this, true);
            Save();
        }

        #endregion
    }
}