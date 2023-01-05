using System;
using System.Collections.Generic;
using System.IO;
using Games.CricketGame.Manager.Code.Manager;
using Nico.Common;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Games.CricketGame.Manager.Code.Pokemon.Skill
{
    [Serializable]
    public class SkillMeta
    {
        #region STATIC

        private static bool _initialized = false;
        private static readonly string _meta_map_path = "skill/meta_map.json";
        private static Dictionary<SkillEnum, SkillMeta> _metaMap;
        
        private static void InitializeStatic()
        {
            
            try
            {
                _metaMap = JsonResourcesManager.LoadStreamingAssets<Dictionary<SkillEnum, SkillMeta>>(_meta_map_path);
                if (_metaMap == null)
                {
                    _metaMap = new();
                }
            }
            catch (FileNotFoundException)
            {
                _metaMap = new();
            }

            _initialized = true;
        }
        public static void Save()
        {
            JsonResourcesManager.SaveStreamingAssets(_metaMap, _meta_map_path,true);
        }

        public static List<SkillEnum> GetSkillEnumList(bool reload = false)
        {
            if (reload)
            {
                InitializeStatic();
            }
            else if (!_initialized)
            {
                InitializeStatic();
            }
            return new List<SkillEnum>(_metaMap.Keys);

        }

        public static void Add(SkillMeta meta, bool replace = true)
        {
            if (!_initialized)
            {
                InitializeStatic();
            }
            
            if (!_metaMap.ContainsKey(meta.skillEnum))
            {
                _metaMap.Add(meta.skillEnum, meta);
            }
            else if(replace)
            {
                //Debug.LogWarning("正在覆盖之前的skillMeta数据");
                _metaMap[meta.skillEnum] = meta;
            }
            else
            {
                throw new KeyNotFoundException("已经存在对应skillMeta,且未配置replace");
            }
        }

        public static void Clear()
        {
            _metaMap.Clear();
        }


        public static SkillMeta Find(SkillEnum skillEnum)
        {
            if (!_initialized)
            {
                InitializeStatic();
            }

            if (_metaMap.ContainsKey(skillEnum))
            {
                return _metaMap[skillEnum];
            }
            else
            {
                return new SkillMeta();
            }

        }


        #endregion

        
        public string name;
        public SkillEnum skillEnum;
        public PriorityEnum priority;
        public int power;
        public float hitRate;

        public SkillMeta()
        {
            this.skillEnum = SkillEnum.None;
        }
        public SkillMeta(string name, SkillEnum skillEnum, int power, float hitRate,PriorityEnum? priority,bool autoAdd)
        {
            this.name = name;
            this.skillEnum = skillEnum;
            this.power = power;
            this.hitRate = hitRate;
            if (priority != null)
            {
                this.priority = (PriorityEnum)priority;
            }
            else
            {
                this.priority = PriorityEnum.正常手;
            }

            if (autoAdd)
            {
                Add(this);
            }

        }
    }
}