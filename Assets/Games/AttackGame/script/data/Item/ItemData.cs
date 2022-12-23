using System.Collections;
using UnityEngine;

namespace AttackGame
{
    [CreateAssetMenu(fileName = "DroppedItem", menuName = "Data/Item/DroppedItem", order = 0)]
    public class ItemData : ScriptableObject
    {
        public uint uid;
        public string item_name;
        public string desc;
        public uint package_limit;//一格背包存放这个物品上限
        public uint holding_limit;//持有上限
        public Sprite sprite;
    }
}