using System;
using UnityEngine;

namespace AttackGame
{
    /// <summary>
    /// 物品显示在UI上所需的额外数据
    /// </summary>
    [Serializable]
    public class ItemInfo
    {
        public int num; //数量
        public Sprite sprite;
    }
    
    /// <summary>
    /// 用于在inspector面板上初始化物品(DEBUG)的物品数据存储结构体
    /// </summary>
    [Serializable]
    public class InspectorItem
    {
        public ItemData data;
        public ItemInfo info;
    }
}