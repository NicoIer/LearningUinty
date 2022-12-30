using System;
using System.Collections.Generic;
using AttackGame._Item;
using AttackGame.common.component;
using AttackGame.UI;
using UnityEngine;

namespace AttackGame.Player
{

    [Serializable]
    public class ItemPair
    {
        public ItemPair(ItemData data,int num)
        {
            this.data = data;
            this.num = num;
        }
        public ItemData data;
        public int num;
    }
    /// <summary>
    /// 玩家详情
    /// </summary>
    public class PlayerInfo: ICoreComponent
    {
        public float health;//玩家生命值
        public Dictionary<uint,ItemPair> items = new();//玩家持有的道具集合

        public void Start()
        {
            UIManager.instance.packageManager.dropAction += DropItem;
            UIManager.instance.packageManager.addAction += AddItem;
        }

        private void AddItem(ItemData data, int num)
        {
            if (items.ContainsKey(data.uid))
            {
                items[data.uid].num += num;
            }
            else
            {
                var pair = new ItemPair(data, num);
                items.Add(data.uid,pair);
            }
        }
        private void DropItem(ItemData data, int num)
        {
            if (num == -1)
            {
                items.Remove(data.uid);
            }
            else
            {
                items[data.uid].num -= num;
            }
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            
        }
    }
}