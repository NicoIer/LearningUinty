using System;
using System.Collections;
using UnityEngine;

namespace PackageGame
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item", order = 0)]
    [Serializable]
    public class ItemData : ScriptableObject
    {
        public uint uid;
        public string item_name;
        public string desc;
        public uint package_limit; //一格背包存放这个物品上限
        public uint holding_limit; //持有上限
    }
}