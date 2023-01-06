using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Games.CricketGame.Cricket_
{
    public class Property
    {//ToDo 后续再做这一块
        private Dictionary<PropertyEnum, string> _iconMap;
        private PropertyEnum _propertyEnum;
        [JsonIgnore]public Sprite sprite;
        
        
    }
}