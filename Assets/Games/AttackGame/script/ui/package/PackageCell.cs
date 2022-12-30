using System;
using AttackGame._Item;
using AttackGame.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AttackGame.Package
{
    public class PackageCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        //ToDo 实现 被选中 快捷丢弃 功能

        #region UI Attribute

        private Text _count_text;
        private Text _name_text;
        private Image _image;
        private Image _mask_image;
        private Button _button;

        #endregion

        #region Data Attribute

        public Item item { get; private set; }

        #endregion

        public bool empty { get; private set; } = true;
        private int _idx;
        private bool _selected;

        public bool selected
        {
            get => _selected;
            set
            {
                _selected = value;

                _mask_image.gameObject.SetActive(value);
            }
        }

        #region Set Method

        public void set_idx(int idx)
        {
            _idx = idx;
        }

        #endregion

        #region Unity Method

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnBtnClicked);


            _image = transform.GetChild(0).GetComponent<Image>();
            _count_text = transform.GetChild(1).GetComponent<Text>();
            _name_text = transform.GetChild(2).GetComponent<Text>();
            _mask_image = transform.GetChild(3).GetComponent<Image>();
        }

        #endregion

        #region Event Method

        private void OnBtnClicked()
        {
            if (!empty)
            {
                UIManager.instance.packageManager.ClickedCell(_idx);
                selected = true;
            }
            else
            {
                UIManager.instance.packageManager.ClickedCell();
                selected = false;
            }
            //UpdateCell(empty = true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!empty)
            {
                UIManager.instance.packageManager.PointerEnterCell(_idx);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.instance.packageManager.PointerExitCell(_idx);
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
                _image.sprite = item.sprite;
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
            {
                //为空返回-1 表示为空
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
            UpdateCell(true);
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
            if (empty)
            {
                //如果格子为空 
                if (item.data.package_limit >= item.num)
                {
                    //可以直接放下 则放下
                    this.item = item;
                    _set_item_print(item);
                    UpdateCell(false);
                    return LeftCapacity();
                }

                //放一个假的
                this.item = new Item(item.data, 0,item.sprite);


                //一次性放不下
                return -2;
            }

            if (this.item.data.uid == item.data.uid)
            {
                //不为空 但存放的物品相同 则 叠加放
                if (LeftCapacity() >= item.num)
                {
                    //放的下
                    _set_item_print(item);
                    
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

        private void _set_item_print(Item item)
        {
           
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
            if (empty)
            {
                return -1;
            }

            if (item != null)
            {
                var tmp = (int)item.data.package_limit - item.num;
                if (tmp < 0)
                    throw new IndexOutOfRangeException($"背包格还能存放:{tmp} < 0个{item.data.item_name}");
                return tmp;
            }

            throw new NullReferenceException("背包格的物品没有设定");
        }

        /// <summary>
        /// 当前格子是否存满
        /// </summary>
        /// <returns></returns>
        public bool Full()
        {
            if (empty)
            {
                return false;
            }

            if (item != null)
            {
                return item.data.package_limit >= item.num;
            }

            throw new IndexOutOfRangeException("不为空的背包格持有了null物品!!!");
        }

        #endregion
    }
}