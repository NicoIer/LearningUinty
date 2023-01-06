using System;
using System.Collections.Generic;
using System.IO;
using Games.CricketGame.Code.Cricket_;
using Newtonsoft.Json;
using Nico.Common;
using UnityEngine;

namespace Games.CricketGame.Cricket_
{
    [Serializable]
    public class SkillInspector
    {
        public SkillEnum skillEnum;
        public PropertyEnum propertyEnum;
        public int useTimes;
        public int needLevel;
    }

    [Serializable]
    public class Skill
    {
        #region Static

        public static bool Initilized { get; private set; }
        private static Dictionary<SkillEnum, Type> _effectTyeMap;
        private static Dictionary<SkillEnum, ISkillEffect> _effectObjs;
        private static readonly string _effect_map_path = "skill/effect_map.json";

        #endregion


        #region Static Method

        public static Dictionary<SkillEnum, Type> GetEffectMap(bool reload=false)
        {
            if (reload)
            {
                InitializeStatic();
            }
            else if (!Initilized)
            {
                InitializeStatic();
            }

            return _effectTyeMap;
        }

        public static void ClearEffect()
        {
            _effectTyeMap.Clear();
            _effectObjs.Clear();
        }

        private static void InitializeStatic()
        {
            //初始化静态变量
            try
            {
                _effectTyeMap = JsonResourcesManager.LoadStreamingAssets<Dictionary<SkillEnum, Type>>(_effect_map_path);
                if (_effectTyeMap == null)
                {
                    _effectTyeMap = new();
                }
            }
            catch (DirectoryNotFoundException)
            {
                _effectTyeMap = new();
            }
            catch (FileNotFoundException)
            {
                _effectTyeMap = new();
            }

            _effectObjs = new();
            foreach (var (skillEnum, type) in _effectTyeMap)
            {
                //读取枚举 - 效果类型表 通过反射创建对应类型的效果对象
                var effect = (ISkillEffect)Activator.CreateInstance(type);
                _effectObjs.Add(skillEnum, effect);
            }

            Initilized = true;
        }

        public static void AddEffectByString(SkillEnum skillEnum, string type, bool replace)
        {
            if (!Initilized)
            {
                InitializeStatic();
            }

            var value = Type.GetType(type);
            if (value == null)
            {
                Debug.LogError($"请先定义{skillEnum}对应的effect类型");
                return;
            }

            if (!_effectTyeMap.ContainsKey(skillEnum))
            {
                Debug.Log($"为技能:{skillEnum} 添加技能效果类:{value}");
                _effectTyeMap.Add(skillEnum, value);
                _effectObjs.Add(skillEnum, (ISkillEffect)Activator.CreateInstance(value));
            }
            else if (replace)
            {
                _effectTyeMap[skillEnum] = value;
                _effectObjs[skillEnum] = (ISkillEffect)Activator.CreateInstance(value);
                Debug.LogWarning($"覆盖了{skillEnum}的效果为:{value}");
            }
            else
            {
                throw new Exception("未指定replace时出现了key冲突");
            }

            var effect = (ISkillEffect)Activator.CreateInstance(value);
            if (!_effectObjs.ContainsKey(skillEnum))
            {
                _effectObjs.Add(skillEnum, effect);
            }
            else
            {
                _effectObjs[skillEnum] = effect;
            }
        }

        public static void AddEffect<T>(SkillEnum skillEnum, bool replace) where T : ISkillEffect
        {
            Debug.Log($"为技能:{skillEnum} 添加技能效果类:{typeof(T)}");
            if (!Initilized)
            {
                InitializeStatic();
            }

            if (!_effectTyeMap.ContainsKey(skillEnum))
            {
                _effectTyeMap.Add(skillEnum, typeof(T));
                _effectObjs.Add(skillEnum, (ISkillEffect)Activator.CreateInstance(typeof(T)));
            }
            else if (replace)
            {
                _effectTyeMap[skillEnum] = typeof(T);
                _effectObjs[skillEnum] = (ISkillEffect)Activator.CreateInstance(typeof(T));
                Debug.LogWarning($"覆盖了{skillEnum}的效果为:{typeof(T)}");
            }
            else
            {
                throw new Exception("未指定replace时出现了key冲突");
            }
        }

        public static void Save()
        {
            JsonResourcesManager.SaveStreamingAssets(_effectTyeMap, _effect_map_path, true);
        }

        #endregion

        public SkillMeta meta;
        public PropertyEnum propertyEnum;
        [JsonIgnore] public int cur_times { get; private set; }
        public int use_times;
        [HideInInspector]public int need_level;

        public Skill(SkillEnum skillEnum, PropertyEnum propertyEnum, int needLevel, int useTimes)
        {
            if (!Initilized)
            {
                InitializeStatic();
            }

            this.propertyEnum = propertyEnum;
            this.meta = SkillMeta.Find(skillEnum);
            this.need_level = needLevel;
            this.use_times = useTimes;
        }

        public bool Apply(Cricket user, Cricket hitter)
        {
            if (cur_times > 0)
            {
                _effectObjs[meta.skillEnum].Apply(user, hitter, this);
                return true;
            }

            return false;


        }
    }
}