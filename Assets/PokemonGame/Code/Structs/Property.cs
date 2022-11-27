using System;
using System.Collections.Generic;
using PokemonGame.Code.Factory;

namespace PokemonGame.Code.Structs
{




    public class Property
    {
        public static Property find_property(PropertyEnum propertyEnum)
        {
            return PropertyFactory.properties[propertyEnum];
        }

        //TODo 为属性添加描述字符 显示图像
        public string name;
        public PropertyEnum propertyEnum;
        public Dictionary<PropertyEnum, PropertyEffect> restrainDict;

        public Property(string name, PropertyEnum propertyEnum, Dictionary<PropertyEnum, PropertyEffect> restrainDict)
        {
            this.name = name;
            this.propertyEnum = propertyEnum;
            this.restrainDict = restrainDict;
        }
    }
}