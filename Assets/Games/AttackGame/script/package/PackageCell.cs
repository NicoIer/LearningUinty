using System;
using UnityEngine;
using UnityEngine.UI;

namespace AttackGame
{
    public class PackageCell : MonoBehaviour
    {
        //ToDo 实现 被选中 快捷丢弃 功能

        #region UI Attribute

        private Text _count_text;
        private Text _name_text;
        private Image _image;
        private Button _button;

        #endregion

        #region Data Attribute

        public Item item { get; private set; }

        #endregion

        public bool empty { get; private set; } = true;

        public bool selected = false;


        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnBtnClicked);
            _image = transform.GetChild(0).GetComponent<Image>();
            _count_text = transform.GetChild(1).GetComponent<Text>();
            _name_text = transform.GetChild(2).GetComponent<Text>();
        }


        #region Event Method

        private void OnBtnClicked()
        {
            print($"鼠标点击了{name}");
            UpdateCell(empty = true);
        }

        #endregion

        #region UI Method

        private void UpdateCell(bool empty)
        {
            this.empty = empty;
            if (this.empty)
            {
                _image.color = Color.clear;
                _name_text.gameObject.SetActive(false);
                _count_text.gameObject.SetActive(false);
            }
            else
            {
                _image.sprite = item.data.sprite;
                _count_text.text = item.num.ToString();
                _name_text.text = item.data.item_name;
                _image.color = Color.white;
                _name_text.gameObject.SetActive(true);
                _count_text.gameObject.SetActive(true);
            }
        }

        #endregion

        #region Function Method

        public int Count()
        {
            if (empty)
            {//为空返回-1 表示为空
                return -1;
            }
            
            if (item == null)
            {
                throw new NullReferenceException($"cell hold a null item so can't be count");
            }
            //不为空 且持有item 则返回数量
            return item.num;
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
            print($"存放{item.num}个{item.data.item_name}到{name}");
            if (empty)
            {
                //如果格子为空 
                if (item.data.package_limit >= item.num)
                {
                    //可以直接放下 则放下
                    this.item = item;
                    UpdateCell(false);
                    return LeftCapacity();
                }

                //放一个假的
                this.item = new Item(item.data, 0);


                //一次性放不下
                return -2;
            }

            if (this.item.data.uid == item.data.uid)
            {
                //不为空 但存放的物品相同 则 叠加放
                if (LeftCapacity() >= item.num)
                {
                    //放的下
                    this.item.num += item.num;
                    UpdateCell(false);
                    return LeftCapacity();
                }

                //放不下了
                return -2;
            }

            //格子不为空 且 存放的物品不相同
            return -1;
        }


        public bool Remove(int num)
        {
            if (item.num > num)
            {
                item.num -= num;
                UpdateCell(false);
                return true;
            }

            if (item.num == num)
            {
                item.num -= num;
                UpdateCell(true);
                return true;
            }

            //throw new IndexOutOfRangeException($"delete num:{num}  >  cur_num:{Item.num}");
            return false;
        }

        /// <summary>
        /// 获取当前格子剩余可存放对应物品的数量
        /// </summary>
        /// <returns></returns>
        public int LeftCapacity()
        {
            if (item != null)
            {
                var tmp = (int)item.data.package_limit - item.num;
                if (tmp < 0)
                    throw new IndexOutOfRangeException($"背包格还能存放:{tmp}个{item.data.item_name}");
                return tmp;
            }

            throw new NullReferenceException("背包格的物品没有设定");
        }

        #endregion
    }
}