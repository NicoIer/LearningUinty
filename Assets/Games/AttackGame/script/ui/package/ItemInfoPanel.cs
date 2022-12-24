using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AttackGame
{
    public class ItemInfoPanel : MonoBehaviour
    {
        [SerializeField] private Text detail_text;
        [SerializeField] private Image image;
        [SerializeField] private Text count_text;
        [SerializeField] private Text name_text;

        [SerializeField] private Button drop_btn_0;
        [SerializeField] private Button drop_btn_1;
        [SerializeField] private Button use_btn_0;
        [SerializeField] private Button use_btn_1;

        private void Awake()
        {
            gameObject.SetActive(false);
            //Todo 能否使用 Event优化
            drop_btn_0.onClick.AddListener(DropBtnClicked_0);
            drop_btn_1.onClick.AddListener(DropBtnClicked_1);
            use_btn_0.onClick.AddListener(UseBtnClicked_0);
            use_btn_1.onClick.AddListener(UseBtnClicked_1);
        }


        #region UI Method

        public void Flash(Item item)
        {
            detail_text.text = item.desc;
            image.sprite = item.sprite;
            count_text.text = item.num.ToString();
            name_text.text = item.item_name;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        #endregion

        #region Event

        private void DropBtnClicked_0()
        {
            UIManager.instance.packageManager.DropBtnClicked(1);
        }

        private void DropBtnClicked_1()
        {
            
            UIManager.instance.packageManager.DropBtnClicked(-1);
        }

        private void UseBtnClicked_0()
        {
        }

        private void UseBtnClicked_1()
        {
        }

        #endregion
    }
}