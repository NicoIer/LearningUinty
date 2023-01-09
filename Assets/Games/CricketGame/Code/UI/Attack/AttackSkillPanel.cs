using System.Collections.Generic;
using Games.CricketGame.Cricket_;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public class AttackSkillPanel : MonoBehaviour
    {
        private List<SkillUI> _skillButtons = new();
        private bool _connected;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var skillButton = transform.GetChild(i).GetComponent<SkillUI>();
                _skillButtons.Add(skillButton);
            }
        }
        
        public void Connect(CricketData data)
        {
            if (_connected)
            {
                Debug.LogWarning($"已经连接了{data}到{name}!正在断开");
                DisConnect();
            }

            print($"{data.name}连接到{name}");
            _connected = true;
            for (int i = 0; i < _skillButtons.Count; i++)
            {
                _skillButtons[i].Connect(data.skills[i]);
            }
        }

        public void DisConnect()
        {
            print("SkillPanel断开");
            _connected = false;
            for (int i = 0; i < _skillButtons.Count; i++)
            {
                _skillButtons[i].DisConnect();
            }
        }
    }
}