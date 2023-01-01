using System;
using System.Collections.Generic;
using Nico.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Games.CricketGame.Code.Pokemon.Skill
{
    [Serializable]
    public class Skill
    {
        #region Static

        private static bool _initilized = false;
        private static Dictionary<SkillEnum, Type> _effectTyeMap;
        private static Dictionary<SkillEnum, ISkillEffect> _effectMap;
        private static readonly string _effect_map_path = "./data/effect_map.json";

        #endregion

        public SkillMeta meta;
        public int use_times;
        public int need_level;

        private static void InitializeStatic()
        {
            //初始化静态变量
            try
            {
                _effectTyeMap = ResourcesManager.Load<Dictionary<SkillEnum, Type>>(_effect_map_path);
            }
            catch (System.IO.FileNotFoundException e)
            {
                _effectTyeMap = new();
            }

            _effectMap = new();
            foreach (var (skillEnum, type) in _effectTyeMap)
            {
                //读取枚举 - 效果类型表 通过反射创建对应类型的效果对象
                var effect = (ISkillEffect)Activator.CreateInstance(type);
                _effectMap.Add(skillEnum, effect);
            }

            _initilized = true;
        }

        public static void Add<T>(SkillEnum skillEnum) where T: ISkillEffect
        {
            if (!_initilized)
            {
                InitializeStatic();
            }
            if (!_effectTyeMap.ContainsKey(skillEnum))
            {
                _effectTyeMap.Add(skillEnum,typeof(T));
                _effectMap.Add(skillEnum,(ISkillEffect)Activator.CreateInstance(typeof(T)));
            }

        }

        public static void Save()
        {
            ResourcesManager.Save(_effectTyeMap,_effect_map_path);
        }
        public Skill(SkillEnum skillEnum, int needLevel, int useTimes)
        {
            if (!_initilized)
            {
                InitializeStatic();
            }


            this.meta = SkillMeta.Find(skillEnum);
            this.need_level = needLevel;
            this.use_times = useTimes;
        }

        public void Apply(Pokemon user, Pokemon hitter)
        {
            _effectMap[meta.skillEnum].Apply(user, hitter);
        }
    }
}