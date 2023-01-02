using System;
using UnityEngine;

namespace PokemonGame
{
    public enum ItemEnum
    {
        None,
        奇迹种子,
    }

    /// <summary>
    /// 道具
    /// </summary>
    public class Item
    {
        #region STATIC

        public static Item find_item(ItemEnum itemEnum)
        {//ToDo 将这里变成从JSON中读取
            return new Item(itemEnum.ToString(), itemEnum);
        }

        #endregion

        Item(string name, ItemEnum itemEnum)
        {
            this.name = name;
            this.item_enum = itemEnum;
        }

        //ToDo 添加道具的其他效果
        public string name;
        public ItemEnum item_enum;
        public Sprite icon;
    }
}