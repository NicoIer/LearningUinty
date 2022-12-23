using System;
using System.Collections.Generic;
using UnityEngine;

namespace AttackGame
{
    /// <summary>
    /// 背包管理器 用于 进行UI和数据的交互
    /// </summary>
    public class PackageManager : MonoBehaviour
    {
        [SerializeField] private List<PackageCell> cells = new(); //所有的背包单元格集合

        private List<Item> _items = new(); //玩家所持有的所有道具

        //当前选中的背包格索引
        public int _cur_cell_idx { get; set; }

        private GameObject _cell_panel;

        private void Awake()
        {
            _cell_panel = transform.GetChild(0).gameObject;
            for (var i = 0; i < _cell_panel.transform.childCount; i++)
            {
                cells.Add(_cell_panel.transform.GetChild(i).GetComponent<PackageCell>());
            }

            _cur_cell_idx = 0;
        }

        /// <summary>
        /// 返回下一个可存放对应物品的背包格索引
        /// </summary>
        /// <returns>-1表示没有可用的背包格</returns>
        public int NextAvailableCellIndex()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].empty)
                {
                    print($"获取到可用的Index:{i} total cell:{cells.Count}");
                    return i;
                }
            }

            print($"获取到可用的Index:{-1} total cell:{cells.Count}");
            return -1;
        }

        /// <summary>
        /// 返回下一个可存放对应物品的背包格索引
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int NextAvailableCellIndex(Item item)
        {
            for (var i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                if (cell.empty)
                {
                    //为空 当然可以存放
                    return i;
                }

                if (cell.Item.data.uid == item.data.uid && cell.LeftCapacity() > 0)
                {
                    //存放的物品相同 且 还有剩余位置
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 显示物品
        /// </summary>
        /// <param name="item"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public void DisPlayItem(Item item, int position = -1)
        {
            print($"存放:{item.data.item_name}");
            if (position == -1)
            {
                position = NextAvailableCellIndex();
            }

            var total = item.num;
            var limit = item.data.package_limit;
            if (total > limit)
            {
                var left = total % limit;
                var times = total / limit;
                for (int i = 0; i < times; i++)
                {
                    position = NextAvailableCellIndex();
                    var item1 = new Item
                    {
                        data = item.data,
                        num = (int)limit
                    };
                    cells[position].SetItem(item1);
                }

                if (left != 0)
                {
                    position = NextAvailableCellIndex();
                    var item2 = new Item
                    {
                        data = item.data,
                        num = (int)left
                    };
                    cells[position].SetItem(item2);
                }
            }
            else
            {
                cells[position].SetItem(item);
            }
        }
    }
}