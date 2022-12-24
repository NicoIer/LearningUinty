using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AttackGame
{
    public class GameManager : Script.Tools.DesignPattern.Singleton<GameManager>
    {
        [Header("DEBUG")] public bool paused;

        [Header("用于调试背包的临时物品信息")] public InspectorItem testItem;


        [Header("初始化背包所需信息")] public List<InspectorItem> inspectorItems = new();

        //测试用 物品列表应该由玩家持有
        private readonly List<Item> _items = new();

        protected void Start()
        {
            InitData();
            DisplayData();
        }

        private void InitData()
        {
            foreach (var inspectorItem in inspectorItems)
            {
                var item = new Item(inspectorItem.data, inspectorItem.info.num);
                _items.Add(item);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var item = new Item(testItem.data, testItem.info.num);

                UIManager.instance.packageManager.AddItem(item);
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                var item = new Item(testItem.data, testItem.info.num);

                UIManager.instance.packageManager.RemoveItem(item.uid, 1);
            }
        }

        private void DisplayData()
        {
            foreach (var item in _items)
            {
                UIManager.instance.packageManager.AddItem(item);
            }
        }
    }
}