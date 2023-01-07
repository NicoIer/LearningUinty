using Games.CricketGame.Code.Cricket_;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public class AttackOperatorPanel : MonoBehaviour
    {
        public SelectPanel selectPanel;
        public SkillPanel skillPanel;
        private bool _initialized;
        private CricketData _data;

        public void Initialize(CricketData data)
        {
            _initialized = true;
            _data = data;
            selectPanel.gameObject.SetActive(true);
            selectPanel.attackClicked += _enter_skill_panel;
            skillPanel.gameObject.SetActive(false);
        }

        public void RoundStart()
        {
            if (!_initialized)
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
            print("进入技能选择面板");
            skillPanel.Initialize(_data);
        }
    }
}