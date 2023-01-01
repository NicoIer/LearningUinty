using System;
using System.Collections.Generic;
using Nico.Common;

namespace Games.CricketGame.Code.Pokemon.Skill
{
    [Serializable]
    public class SkillMeta
    {
        private static bool _initialized = false;
        private static readonly string _meta_map_path = "./data/skill_meta_map.json";
        private static Dictionary<SkillEnum, SkillMeta> _metaMap;

        private static void InitializeStatic()
        {
            try
            {
                _metaMap = ResourcesManager.Load<Dictionary<SkillEnum, SkillMeta>>(_meta_map_path);
            }
            catch (Exception e)
            {
                _metaMap = new();
            }

            _initialized = true;
        }
        

        public static void Add(SkillMeta meta)
        {
            if (!_initialized)
            {
                InitializeStatic();
            }
            if (!_metaMap.ContainsKey(meta.skillEnum))
            {
                _metaMap.Add(meta.skillEnum, meta);
            }
        }
        public static void Save()
        {
            ResourcesManager.Save(_metaMap, _meta_map_path);
        }

        public static SkillMeta Find(SkillEnum skillEnum)
        {
            if (!_initialized)
            {
                InitializeStatic();
            }

            return _metaMap[skillEnum];
        }

        public string name;
        public SkillEnum skillEnum;
        public int power;
        public float hitRate;
    }
}