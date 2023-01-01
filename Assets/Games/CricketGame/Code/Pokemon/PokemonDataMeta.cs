using System;
using System.Collections.Generic;
using Games.CricketGame.Code.Pokemon.Skill;
using Nico.Common;

namespace Games.CricketGame.Code.Pokemon
{
    [Serializable]
    public class PokemonDataMeta
    {
        #region Static Attribute

        private static Dictionary<PokemonEnum, PokemonDataMeta> _pokemonMeta;
        private static readonly string _pokemon_meta_path = "./data/pokemonMeta.json";
        private static bool _initilized = false;

        #endregion

        #region Static Method

        private static void InitializeStatic()
        {
            try
            {
                _pokemonMeta = ResourcesManager.Load<Dictionary<PokemonEnum, PokemonDataMeta>>(_pokemon_meta_path);
            }
            catch (Exception e)
            {
                _pokemonMeta = new();
            }
            _initilized = true;
        }

        public static void Save()
        {
            ResourcesManager.Save(_pokemonMeta, _pokemon_meta_path);
        }

        public static void Add(PokemonDataMeta meta)
        {
            if (!_initilized)
            {
                InitializeStatic();
            }
            if (!_pokemonMeta.ContainsKey(meta.pokemonEnum))
            {
                _pokemonMeta.Add(meta.pokemonEnum,meta);
            }
        }

        public static PokemonDataMeta Find(PokemonEnum pokemonEnum)
        {
            if (!_initilized)
            {
                InitializeStatic();
            }
            return _pokemonMeta[pokemonEnum];
        }

        #endregion

        public string defaultName;
        public PokemonProperty property;
        public PokemonEnum pokemonEnum;
        public int healthRace;
        public int attackRace;
        public int defenseRace;
        public int speedRace;
        public Dictionary<SkillEnum, Skill.Skill> SkillMetas;
        public PokemonEnum nextLevel;
    }
}