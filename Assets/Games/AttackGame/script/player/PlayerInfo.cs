using System.Collections;
using System.Collections.Generic;
using AttackGame._Item;
using UnityEngine;

namespace AttackGame.Player
{
    /// <summary>
    /// 玩家详情
    /// </summary>
    public class PlayerInfo
    {
        public float health;//玩家生命值
        public List<Item> items = new();//玩家持有的道具集合
        
    }
}