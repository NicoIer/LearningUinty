using System;

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
        {
            switch (itemEnum)
            {
                case ItemEnum.None:
                    return new Item("", itemEnum);
                case ItemEnum.奇迹种子:
                    return new Item("奇迹种子", itemEnum);
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemEnum), itemEnum, null);
            }
            
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
    }
}