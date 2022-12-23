using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AttackGame
{
    public class GameManager : Script.Tools.DesignPattern.Singleton<GameManager>
    {
        [Header("DEBUG")] public InspectorItem testItem;

        public bool paused;

        public List<InspectorItem> inspectorItems = new();
        private List<Item> _items = new();

        protected override void Awake()
        {
            base.Awake();
            InitData();
            DisplayData();
        }

        private void InitData()
        {
            foreach (var inspectorItem in inspectorItems)
            {
                var item = new Item
                {
                    data = inspectorItem.data,
                    num = inspectorItem.info.num
                };
                _items.Add(item);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var item = new Item
                {
                    data = testItem.data,
                    num = testItem.info.num
                };
                UIManager.instance.packageManager.DisPlayItem(item);
            }
        }

        private void DisplayData()
        {
            foreach (var item in _items)
            {
                print(item.data.desc);
                UIManager.instance.packageManager.DisPlayItem(item);
            }
        }
    }
}