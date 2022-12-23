namespace AttackGame
{
    /// <summary>
    /// 道具类
    /// </summary>
    public class Item
    {
        public uint uid => data.uid;
        public ItemData data;//道具的基本数据
        public int num;//持有的数量
    }
}