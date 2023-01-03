using System;
using System.Collections.Generic;
using System.IO;
using Games.CricketGame.Code.Pokemon.Enum;
using Nico.Common;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon
{
    [Serializable]
    public class PokemonDataMeta
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
                    JsonResourcesManager.LoadStreamingAssets<Dictionary<PokemonEnum, PokemonDataMeta>>(_pokemon_meta_path);
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
            JsonResourcesManager.SaveStreamingAssets(_pokemonMeta, _pokemon_meta_path,true);
        }

        public static void Add(PokemonDataMeta meta,bool replace)
        {
            if (!_initilized)
            {
                InitializeStatic();
            }
            if (!_pokemonMeta.ContainsKey(meta.pokemonEnum))
            {
                _pokemonMeta.Add(meta.pokemonEnum,meta);
            }else if (replace)
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
        
        public PropertyEnum property;
        public PokemonEnum pokemonEnum;
        public int healthRace;
        public int attackRace;
        public int defenseRace;
        public int specialAttackRace;
        public int specialDefenseRace;
        public int speedRace;
        public string desc;//图鉴描述
        public Dictionary<SkillEnum, Skill.Skill> SkillMetas;
        public PokemonEnum previousLevel;
        public List<PokemonEnum> nextLevel = new();
    }
}