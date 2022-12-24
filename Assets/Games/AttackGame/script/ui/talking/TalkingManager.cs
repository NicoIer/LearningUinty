using System;
using AttackGame.Data.Talking;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace AttackGame
{
    public class TalkingManager : MonoBehaviour
    {
        [SerializeField] private Text _taking_text;

        [SerializeField] private Image _background_image;

        //ToDo 后续构建一个新的mono类控制左右对话的显示
        [SerializeField] private GameObject right;
        [SerializeField] private GameObject left;
        private TalkingData _data;
        private int _idx = -1; //当前显示到的对话索引
        public bool opened { get; set; }

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
            print("Open");
            opened = true;
            _data = data;
            data.Init();//ToDo 搞清楚这里为什么必须要Init一下
            gameObject.SetActive(true);
            NextData();
        }

        public void Close()
        {
            print("关闭");
            opened = false;
            _idx = -1;
            gameObject.SetActive(false);
        }

        private void NextData()
        {
            print("Next");
            _idx++;
            print(_data.Count);
            if (_idx < _data.Count && _idx >= 0)
            {
                var curTalking = _data[_idx];
                _taking_text.text = curTalking.text;
                _background_image.sprite = curTalking.background_image;
            }
            else
            {
                _idx = -1;
                Close();
            }
        }
    }
}