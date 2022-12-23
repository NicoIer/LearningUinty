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
        [SerializeField] private int try_times = 100;
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

            Debug.LogWarning("没有额外的可用背包格子辣!!!");
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
            if (position == -1)
            {
                position = NextAvailableCellIndex(item);
            }

            var total = item.num;
            var limit = item.data.package_limit;
            if (total > limit)
            {
                var left = total % limit;
                var times = total / limit;
                for (int i = 0; i < times; i++)
                {
                    position = NextAvailableCellIndex(item);
                    var item1 = new Item
                    {
                        data = item.data,
                        num = (int)limit
                    };
                    AddItem(item1, position);
                }

                if (left != 0)
                {
                    position = NextAvailableCellIndex(item);
                    var item2 = new Item
                    {
                        data = item.data,
                        num = (int)left
                    };
                    AddItem(item2, position);
                }
            }
            else
            {
                AddItem(item, position);
            }
        }

        public void AddItem(Item item, int position)
        {

            for (int i = 0; i < try_times; i++)
            {
                //要考虑到堆叠的情况
                var curCell = cells[position]; //获取要存放物品的背包格
                var left = curCell.SetItem(item); //尝试存放 
                if (left >= 0)
                {
                    //存放成功
                    break;
                }

                switch (left)
                {
                    case -2:
                    {
                        //可以存 但是只能存一点点
                        //先存一点
                        var item1 = new Item
                        {
                            data = item.data,
                            num = curCell.LeftCapacity()
                        };
                        curCell.SetItem(item1);
                        //再找下一个位置存
                        position = NextAvailableCellIndex(item);
                        item.num -= item1.num;
                        continue;
                    }
                    case -1:
                        //这个位置不能存
                        throw new IndexOutOfRangeException($"不存在的情况:NextAvailableCellIndex(item)必然可以获取一个可用的位置" +
                                                           $"item-uid:{item.data.uid}" +
                                                           $"target-position:{position}" +
                                                           $"target-item-uid:{curCell.Item.data.uid}");
                    default:
                        throw new IndexOutOfRangeException($"不存在的left值:{left}");
                }
            }

            throw new IndexOutOfRangeException($"无法找到合适的位置存放物品After:{try_times} times try");
        }
    }
}