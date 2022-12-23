using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace AttackGame
{
    public class PackageCell : MonoBehaviour
    {
        private Text _count_text;
        private Text _name_text;
        private Image image { get; set; }

        public Item Item { get; private set; }

        /// <summary>
        /// Cell empty属性
        /// 设置值时会自动设置UI元素的active属性
        /// </summary>
        public bool empty { get; private set; } = true;


        private void Awake()
        {
            image = transform.GetChild(0).GetComponent<Image>();
            _count_text = transform.GetChild(1).GetComponent<Text>();
            _name_text = transform.GetChild(2).GetComponent<Text>();
        }
        

        /// <summary>
        /// 清空格子中显示的内容
        /// </summary>
        public void Clear()
        {
            empty = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// >=0表示存放成功
        /// -1表示不能存放
        /// -2表示可以存放 但是不能全部放下
        /// </returns>
        public int SetItem(Item item)
        {
            print($"存放:{item.data.item_name} num:{item.num}");
            if (empty)
            {
                //如果格子为空 则直接放 
                Item = item;
                UpdateCell(false);
                return LeftCapacity();
            }

            if (Item.data.uid == item.data.uid)
            {
                //不为空 但存放的物品相同 则 叠加放
                if (LeftCapacity() >= item.num)
                {
                    //放的下
                    Item.num += item.num;
                    UpdateCell(false);
                    return LeftCapacity();
                }

                //放不下了
                return -2;
            }

            //格子不为空 且 存放的物品不相同
            return -1;
        }

        private void UpdateCell(bool empty)
        {
            this.empty = empty;
            if (this.empty)
            {
                image.color = Color.clear;
                _name_text.gameObject.SetActive(false);
                _count_text.gameObject.SetActive(false);
            }
            else
            {
                image.sprite = Item.data.sprite;
                _count_text.text = Item.num.ToString();
                _name_text.text = Item.data.item_name;
                image.color = Color.white;
                _name_text.gameObject.SetActive(true);
                _count_text.gameObject.SetActive(true);
            }

        }

        /// <summary>
        /// 获取当前格子剩余可存放对应物品的数量
        /// </summary>
        /// <returns></returns>
        public int LeftCapacity()
        {
            if (Item != null)
            {
                return (int)Item.data.package_limit - Item.num;
            }

            throw new NullReferenceException("背包格的物品没有设定");
        }
    }
}