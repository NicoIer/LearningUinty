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

        private void Awake()
        {
            selectPanel.gameObject.SetActive(true);
            //ToDo 其他按钮点击事件也要进行绑定
            selectPanel.attackClicked += _enter_skill_panel;

            skillPanel.gameObject.SetActive(false);
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
            skillPanel.gameObject.SetActive(true);
        }

        private void _back_select_panel()
        {
            selectPanel.gameObject.SetActive(true);
            skillPanel.gameObject.SetActive(false);
        }
    }
}