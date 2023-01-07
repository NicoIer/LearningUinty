using System;
using Games.CricketGame.Code.Cricket_;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public class AttackOperatorPanel : MonoBehaviour
    {
        public SelectPanel selectPanel;
        public SkillPanel skillPanel;
        private bool _connected;
        private GameObject curPanel;

        private void Awake()
        {
            curPanel = selectPanel.gameObject;
            curPanel.SetActive(true); //默认激活选择Panel
            //ToDo 其他按钮点击事件也要进行绑定
            selectPanel.attackClicked += _enter_skill_panel;

            skillPanel.gameObject.SetActive(false); //默认关闭技能Panel
        }


        private void Update()
        {
            if (UIManager.instance.escDown)
            {
                _back_select_panel();
            }
        }

        public void Connect(CricketData data)
        {
            print($"{data.name}连接到{name}");
            _connected = true;
            skillPanel.gameObject.SetActive(true);
            skillPanel.Connect(data);
        }

        public void DisConnect()
        {
            print("AttackPanel断开");
            skillPanel.DisConnect();
            _connected = false;
        }

        public void RoundStart()
        {
            if (!_connected)
            {
                return;
            }

            selectPanel.gameObject.SetActive(true);
            skillPanel.gameObject.SetActive(false);
        }


        private void _enter_skill_panel()
        {
            selectPanel.gameObject.SetActive(false);
            curPanel = skillPanel.gameObject;
            curPanel.SetActive(true);
        }

        private void _back_select_panel()
        {
            print("按下Esc");
            if (curPanel != selectPanel.gameObject)
            {
                curPanel.SetActive(false);
                curPanel = selectPanel.gameObject;
                curPanel.SetActive(true);
            }
        }
    }
}