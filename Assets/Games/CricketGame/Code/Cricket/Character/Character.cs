using System;
using System.Collections.Generic;
using Games.CricketGame.Manager.Code.Manager;
using Newtonsoft.Json;
using Nico.Common;
using UnityEngine;

namespace Games.CricketGame.Manager.Code.Pokemon.Character
{
    [Serializable]
    public class CharacterEffect
    {
        public AbilityEnum abilityEnum;
        public float rate;
    }

    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CricketGame/Pokemon/CharacterData", order = 0)]
    [Serializable]
    public class Character : ScriptableObject
    {
        //ToDo 做成编辑器的形式

        #region STATIC

        private static bool _initialized;
        private static Dictionary<CharacterEnum, Character> _map;
        private static string _mapPath = "pokemon/character.json";

        public static void Initialize()
        {
            try
            {
                _map = JsonResourcesManager.LoadStreamingAssets<Dictionary<CharacterEnum, Character>>(_mapPath);
                if (_map == null)
                    _map = new();
            }
            catch (Exception)
            {
                _map = new Dictionary<CharacterEnum, Character>();
            }

            _initialized = true;
        }

        public static Character Find(CharacterEnum @enum)
        {
            if (!_initialized)
            {
                Initialize();
            }


            return _map[@enum];
        }

        public static void Add(Character character, bool replace)
        {
            if (!_initialized)
            {
                Initialize();
            }
            if (_map.ContainsKey(character.@enum))
            {
                if (replace)
                {
                    Debug.LogWarning($"正在覆盖{character.@enum}对应数据");
                    _map[character.@enum] = character;
                }
                else
                {
                    throw new ArgumentException($"未指定replace时尝试覆盖{character.@enum}对应数据信息");
                }
            }
            else
            {
                _map.Add(character.@enum, character);
            }
        }

        public static void Save()
        {
            if (!_initialized)
            {
                Initialize();
            }

            JsonResourcesManager.SaveStreamingAssets(_map, _mapPath, true);
        }

        #endregion

        [JsonIgnore] public bool autoCreate = false;
        public CharacterEnum @enum; //特性枚举
        public List<CharacterEffect> effect = new(); //对各项能力值的影响
        public string desc; //描述信息

        private void OnValidate()
        {
            if (!autoCreate)
                return;
            Add(this, true);
            Save();
        }
    }
}