using System;
using System.Collections.Generic;
using PokemonGame.Code.Factory;
using UnityEngine;

namespace PokemonGame.Code.Structs
{




    public class Property
    {
        public static Property find_property(PropertyEnum propertyEnum)
        {
            return PropertyFactory.properties[propertyEnum];
        }
        
        public string name;
        public PropertyEnum propertyEnum;
        public Dictionary<PropertyEnum, PropertyEffect> restrainDict;
        //ToDo 属性描述 和 属性图片需要添加
        public string desc="";
        public Sprite icon=null;
        public Property(string name, PropertyEnum propertyEnum, Dictionary<PropertyEnum, PropertyEffect> restrainDict)
        {
            this.name = name;
            this.propertyEnum = propertyEnum;
            this.restrainDict = restrainDict;
        }
    }
}