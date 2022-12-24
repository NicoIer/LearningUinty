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

        private void Awake()
        {
            gameObject.SetActive(false);
        }

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
    }
}