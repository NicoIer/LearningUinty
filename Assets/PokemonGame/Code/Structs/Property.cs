using System;
using System.Collections.Generic;

namespace PokemonGame.Code.Structs
{




    public class Property
    {
        public static Property find_property(PropertyEnum propertyEnum)
        {
            return new Property(propertyEnum.ToString(), propertyEnum);
        }

        private static Dictionary<PropertyEnum, HashSet<PropertyEnum>> _restrinMap; //属性克制表
        private static Dictionary<PropertyEnum, HashSet<PropertyEnum>> _noUseMap; //属性无效表

        static Property()
        {
            //尝试寻找存储的属性克制文件 如果找不到 或者 出错 则 重新生成
            _restrinMap = new Dictionary<PropertyEnum, HashSet<PropertyEnum>>();
            _noUseMap = new Dictionary<PropertyEnum, HashSet<PropertyEnum>>();
        }

        public static PropertyEffect compare(PropertyEnum attackProperty, PropertyEnum defenseProperty)
        {
            //找到效果拔群的表
            var superSet = _restrinMap[attackProperty];

            if (superSet.Contains(defenseProperty))
            {
                return PropertyEffect.效果拔群;
            }

            //找到效果不好的表
            var lowerSet = _restrinMap[defenseProperty];

            if (lowerSet.Contains(defenseProperty))
            {
                return PropertyEffect.效果不好;
            }

            //找到攻击属性无效的表
            var nouseSet = _noUseMap[attackProperty];
            if (nouseSet.Contains(defenseProperty))
            {
                return PropertyEffect.无效;
            }

            return PropertyEffect.一般般;
        }

        public string name;
        public PropertyEnum propertyEnum;

        Property(string name, PropertyEnum propertyEnum)
        {
            this.name = name;
            this.propertyEnum = propertyEnum;
        }
    }
}