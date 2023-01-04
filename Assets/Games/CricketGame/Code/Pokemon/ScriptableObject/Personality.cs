using System;
using System.Collections.Generic;
using System.IO;
using Games.CricketGame.Code.Pokemon.Enum;
using Newtonsoft.Json;
using Nico.Common;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon
{
    [Serializable]
    public class PersonalityEffectMeta
    {
        public AbilityEnum abilityEnum;
        public float percent;
    }

    [CreateAssetMenu(fileName = "PersonalityData", menuName = "Data/CricketGame/Pokemon/PersonalityData", order = 0)]
    [Serializable]
    public class Personality : ScriptableObject
    {//ToDo 后面做成编辑器的形式
        [JsonIgnore] public bool autoCreate = false;
        private static Dictionary<PersonalityEnum, Personality> _data;
        public static string personality_path = "pokemon/personality.json";
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
            {
                Debug.LogWarning("反复初始化性格表!!");
            }


            try
            {
                _data = JsonResourcesManager
                    .LoadStreamingAssets<Dictionary<PersonalityEnum, Personality>>(personality_path);
                if (_data == null)
                {
                    _data = new Dictionary<PersonalityEnum, Personality>();
                }
            }
            catch (FileNotFoundException)
            {
                _data = new Dictionary<PersonalityEnum, Personality>();
            }
            catch (DirectoryNotFoundException)
            {
                _data = new Dictionary<PersonalityEnum, Personality>();
            }

            _initialized = true;
        }

        public static Personality Find(PersonalityEnum personalityEnum)
        {
            if (!_initialized)
            {
                Initialize();
            }
            if (_data.ContainsKey(personalityEnum))
            {
                return _data[personalityEnum];
            }

            throw new KeyNotFoundException("尝试Find一个不在表中的性格");
        }

        public static void Add(Personality personality, bool replace)
        {
            if (!_initialized)
            {
                Initialize();
            }

            if (_data.ContainsKey(personality.personalityEnum))
            {
                if (replace)
                {
                    Debug.LogWarning($"正在覆盖{personality.personalityEnum}性格数据");
                    _data[personality.personalityEnum] = personality;
                }
                else
                {
                    throw new KeyNotFoundException("未指定replace=true时尝试覆盖性格数据");
                }
            }
            else
            {
                _data.Add(personality.personalityEnum, personality);
            }
        }

        public static void Save()
        {
            if (_data == null)
                return;
            JsonResourcesManager.SaveStreamingAssets(_data, personality_path, true);
        }

        public PersonalityEnum personalityEnum;
        public List<PersonalityEffectMeta> effects = new();


        private void OnValidate()
        {
            if (!autoCreate)
                return;
            Add(this, true);
            Save();
        }
    }
}