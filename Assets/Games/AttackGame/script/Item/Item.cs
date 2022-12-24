namespace AttackGame
{
    /// <summary>
    /// 道具类
    /// </summary>
    public class Item
    {
        public Item(ItemData data, int num)
        {
            this.data = data;
            this.num = num;
        }

        public uint uid => data.uid;
        public ItemData data; //道具的基本数据
        public int num; //持有的数量

        public static Item Copy(Item item)
        {
            return new Item(item.data, item.num);
        }
    }
}