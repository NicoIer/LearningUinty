using Games.CricketGame.Cricket_;
using UnityEngine;
using UnityEngine.Serialization;

namespace Games.CricketGame.UI
{
    public class AttackOperatorPanel : MonoBehaviour
    {
        public SelectPanel selectPanel;
        [FormerlySerializedAs("skillPanel")] public AttackSkillPanel attackSkillPanel;
        private bool _connected;
        private GameObject curPanel;

        private void Awake()
        {
            curPanel = selectPanel.gameObject;
            curPanel.SetActive(true); //默认激活选择Panel
            //ToDo 其他按钮点击事件也要进行绑定
            selectPanel.attackClicked += _enter_skill_panel;

            attackSkillPanel.gameObject.SetActive(false); //默认关闭技能Panel
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
            attackSkillPanel.gameObject.SetActive(true);
            attackSkillPanel.Connect(data);
        }

        public void DisConnect()
        {
            print("AttackPanel断开");
            attackSkillPanel.DisConnect();
            _connected = false;
        }

        public void RoundStart()
        {
            if (!_connected)
            {
                return;
            }

            selectPanel.gameObject.SetActive(true);
            attackSkillPanel.gameObject.SetActive(false);
        }
        
        private void _enter_skill_panel()
        {
            selectPanel.gameObject.SetActive(false);
            curPanel = attackSkillPanel.gameObject;
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