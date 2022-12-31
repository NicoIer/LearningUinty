using System;
using System.Collections.Generic;
using PackageGame.Common.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace PackageGame.Talking
{
    public class TalkingManager : MonoBehaviour
    {
        [SerializeField] private Text takingText;

        [SerializeField] private Image backgroundImage;

        //ToDo 后续构建一个新的mono类控制左右对话的显示
        [SerializeField] private GameObject right;

        [SerializeField] private GameObject left;

        //ToDo 用它来控制对话框的样式
        public List<Sprite> styles = new();
        private TalkingData _data;
        private int _idx = -1; //当前显示到的对话索引
        public bool opened { get; private set; }
        private Sprite _defaultStyle;

        private void Awake()
        {
            _defaultStyle = backgroundImage.sprite;
            if (backgroundImage == null)
            {
                backgroundImage = GetComponent<Image>();
            }
        }

        private void Update()
        {
            if (opened)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    NextData();
                }
            }
        }

        public void Open(TalkingData data)
        {
            opened = true;
            _data = data;
            data.Init();
            ResourcesManager.Save(data,"./data/talking/1.json");
            gameObject.SetActive(true);
            NextData();
        }

        public void Close()
        {
            opened = false;
            _idx = -1;
            gameObject.SetActive(false);
        }

        private void NextData()
        {
            _idx++;
            if (_idx < _data.Count && _idx >= 0)
            {
                var curTalking = _data[_idx];
                takingText.text = System.Text.RegularExpressions.Regex.Unescape(curTalking.text);
                backgroundImage.sprite =
                    curTalking.styleIdx < styles.Count ? styles[curTalking.styleIdx] : _defaultStyle;
            }
            else
            {
                _idx = -1;
                Close();
            }
        }
    }
}