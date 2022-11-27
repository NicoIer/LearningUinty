using System;
using System.Collections.Generic;
using PokemonGame.Code.Manager;
using PokemonGame.Code.Structs;
using UnityEngine;

namespace PokemonGame.Code.Factory
{
    public class PropertyFactory : MonoBehaviour
    {
        [Serializable]
        public struct Storage
        {
            public PropertyEnum attack;
            public PropertyEnum defense;
            public PropertyEffect effect;
            public string desc;
            public Sprite sprite;
        }

        public static PropertyFactory instance;
        public static Dictionary<PropertyEnum, Property> properties;
        public static string key = "property.json";
        [Header("属性配置")]
        public List<Storage> restrainMap;
        static PropertyFactory()
        {
            properties = GameSettings.load_properties(key);
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            if (properties == null)
            {
                Debug.LogWarning("未找到属性映射表,正在根据信息创建!!!");
                var map = new Dictionary<PropertyEnum, Dictionary<PropertyEnum, PropertyEffect>>();
                foreach (var variable in restrainMap)
                {
                    var attack = variable.attack;
                    var defense = variable.defense;
                    var effect = variable.effect;

                    if (map.ContainsKey(attack))
                    {
                        var set = map[attack];
                        if (!set.ContainsKey(defense))
                        {
                            set.Add(defense, effect);
                        }
                        else
                        {
                            Debug.Log($"生成属性克制表的时,重复声明克制效果 {attack}-{defense}-{effect}");
                        }
                        
                    }
                    else
                    {
                        var set = new Dictionary<PropertyEnum, PropertyEffect> { { defense, effect } };
                        map.Add(attack, set);
                    }
                }

                properties = new Dictionary<PropertyEnum, Property>();
                foreach (var (p, set) in map)
                {
                    properties.Add(p, new Property(p.ToString(), p, set));
                }
                
                properties.Add(PropertyEnum.无属性,new Property(PropertyEnum.无属性.ToString(),PropertyEnum.无属性,null));
                
                GameSettings.save_properties(properties,key);
            }
            else
            {
                Debug.Log("属性克制表加载完成");
            }
        }
    }
}