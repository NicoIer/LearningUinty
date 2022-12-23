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

        private Item _item;

        public Item Item
        {
            get => _item;
            private set => _item = value;
        }

        private bool _empty = true;

        public int Count => Item?.num ?? 0;

        public bool empty
        {
            get => _empty;
            set
            {
                _empty = value;
                if (_empty)
                {
                    image.color = Color.clear;
                    _name_text.gameObject.SetActive(false);
                    _count_text.gameObject.SetActive(false);
                }
                else
                {
                    image.color = Color.white;
                    _name_text.gameObject.SetActive(true);
                    _count_text.gameObject.SetActive(true);
                }
            }
        }


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

        public bool SetItem(Item item)
        {
            if (empty || _item.data.uid == item.data.uid)
            {
                //如果格子为空 或者 存放的物品相同 则直接放 
                //ToDo 考虑一下物品上限的问题
                Item = item;
                empty = false;
                image.sprite = _item.data.sprite;
                _count_text.text = Count.ToString();
                _name_text.text = item.data.item_name;
                return true;
            }

            //格子不为空 且 存放的物品不相同
            return false;
        }

        /// <summary>
        /// 获取当前格子剩余可存放对应物品的数量
        /// </summary>
        /// <returns></returns>
        public int LeftCapacity()
        {
            if (_item != null)
            {
                return (int)_item.data.package_limit - Count;
            }

            throw new NullReferenceException("背包格的物品没有设定");
        }
    }
}