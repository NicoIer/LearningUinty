using System;

namespace PokemonGame.Code.Structs
{
    /// <summary>
    /// PokemonProperty - 宝可梦的属性
    /// 
    /// </summary>
    [Serializable]
    public enum PropertyEnum
    {
        None, //未定
        Genreal, //一般
        Combat, //格斗
        Flight, //飞行
        Poison, //毒
        Ground, //地面
        Rock, //岩石
        Worm, //虫
        Ghost, //幽灵
        Stell, //钢
        Fire, //火
        Water, //水
        Grass, //草
        Electricity, //电
        Superpowers, //超能
        Ice, //冰
        Dragon, //龙
        Evil, //恶
        Goblin, //妖精
    }
}