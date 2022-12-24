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

        [SerializeField] private int tryTimes = 100;
        //private List<Item> _items = new(); //玩家所持有的所有道具

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

        #region Search Function

        public bool HaveCapacity(uint uid)
        {
            foreach (var cell in cells)
            {
                if (cell.empty)
                {
                    //有空格子 true
                    return true;
                }

                if (cell.item.uid == uid && !cell.Full())
                {
                    //不是空格子 但是 存放的物品相同 且有 剩余空间
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 是否有空的背包格
        /// </summary>
        /// <returns></returns>
        public bool HaveEmpty()
        {
            foreach (var cell in cells)
            {
                if (cell.empty)
                {
                    return true;
                }
            }

            return false;
        }

        public int LastSpecificCellIndex(uint uid)
        {
            for (int i = cells.Count - 1; i >= 0; i--)
            {
                if (!cells[i].empty && cells[i].item.uid == uid)
                {
                    //从后往前找 第一个不为空的 存放相同uid的cell
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 返回下一个为空的物品的背包格索引
        /// </summary>
        /// <returns>-1表示没有可用的背包格</returns>
        public int NextEmptyCellIndex()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].empty)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 返回下一个可存放对应物品的背包格索引
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int NextAvailableCellIndex(uint uid)
        {
            for (var i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                if (cell.empty)
                {
                    //为空 当然可以存放
                    return i;
                }

                if (cell.item.uid == uid && cell.LeftCapacity() > 0)
                {
                    //存放的物品相同 且 还有剩余位置
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #region Item Function

        /// <summary>
        /// 将指定位置的背包格物品减少num个
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="num"></param>
        /// <param name="position"></param>
        public void RemoveItem(uint uid, int num, int position = -1)
        {
            if (position == -1)
            {
                position = LastSpecificCellIndex(uid);
                if (position == -1)
                {
                    throw new IndexOutOfRangeException($"there are on item-uid:{uid} can delete");
                }
            }

            if (!cells[position].Remove(num))
            {
                //删除失败(只可能时因为不够删除)
                var cell = cells[position];
                var left = num;
                for (int i = 0; i < tryTimes; i++)
                {
                    if (left > cell.Count())
                    {
                        left -= cell.Count(); //删除不掉的数量
                        cell.Remove(cell.Count());
                        //找到下一个删除的位置
                        position = LastSpecificCellIndex(uid);
                        cell = cells[position];
                    }
                    else
                    {
                        cell.Remove(left);
                        return;
                    }
                }

                throw new IndexOutOfRangeException($"after {tryTimes} times try remove failed");
            }
        }

        /// <summary>
        /// 添加物品到UI
        /// </summary>
        /// <param name="item"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public void AddItem(Item item, int position = -1)
        {
            if (item.num <= 0)
            {
                //ToDo 是否应该 throw error
                Debug.LogWarning($"{item.num} {item.data.item_name} can't be add");
                return;
            }

            if (position == -1)
            {
                position = NextAvailableCellIndex(item.uid);
                if (position == -1)
                {
                    throw new IndexOutOfRangeException("没有位置可以添加这个物品了");
                }
            }

            _addItem(item, position);
        }

        private void _addItem(Item item, int position)
        {
            for (int i = 0; i < tryTimes; i++)
            {
                //要考虑到堆叠的情况
                var curCell = cells[position]; //获取要存放物品的背包格
                var status = curCell.SetItem(item); //尝试存放 
                if (status >= 0)
                {
                    //存放成功
                    return;
                }

                switch (status)
                {
                    case -2:
                    {
                        //可以存 但是只能存一点点
                        //先存一点
                        var left = curCell.LeftCapacity();
                        if (left == -1)
                        {
                            left = (int)item.data.package_limit;
                        }

                        var item1 = new Item(item.data, left);
                        curCell.SetItem(item1);
                        //再找下一个位置存
                        position = NextAvailableCellIndex(item.uid);
                        item.num -= item1.num;
                        continue;
                    }

                    case -1:
                        //这个位置不能存
                        throw new IndexOutOfRangeException($"不存在的情况:NextAvailableCellIndex(item)必然可以获取一个可用的位置" +
                                                           $"item-uid:{item.data.uid}" +
                                                           $"target-position:{position}" +
                                                           $"target-item-uid:{curCell.item.data.uid}");
                    default:
                        throw new IndexOutOfRangeException($"不存在的left值:{status}");
                }
            }

            throw new IndexOutOfRangeException($"无法找到合适的位置存放物品After:{tryTimes} times try");
        }

        #endregion
    }
}