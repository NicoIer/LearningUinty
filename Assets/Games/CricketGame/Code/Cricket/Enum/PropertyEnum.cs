using System.Collections.Generic;
using UnityEngine;

namespace Games.CricketGame.Cricket_
{
    public enum PropertyEnum
    {
        普通,
        水,
        火,
        草,
        风
    }

    public static class PropertyMap
    {
        private static Dictionary<PropertyEnum, Dictionary<PropertyEnum, float>> attackMap = new()
        {
            {
                PropertyEnum.水, new()
                {
                    { PropertyEnum.火, 1.5f }
                }
            }
        };

        public static float QueryAttack(PropertyEnum attack, PropertyEnum defense)
        {
            if (!attackMap.ContainsKey(attack))
            {
                // Debug.LogWarning($"属性{attack}未存在克制表中");
                return 1;
            }
            return attackMap[attack][defense];
        }
    }
}