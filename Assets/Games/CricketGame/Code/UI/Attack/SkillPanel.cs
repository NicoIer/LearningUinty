using System;
using System.Collections.Generic;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Cricket_;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public class SkillPanel : MonoBehaviour
    {
        public List<SkillButton> skillButtons;
        private bool _initialized;
        private CricketData _data;
        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var skillButton = transform.GetChild(i).GetComponent<SkillButton>();
                skillButtons.Add(skillButton);
            }
        }

        private void OnEnable()
        {
            print("skillPanel启用");
            foreach (var t in skillButtons)
            {
                t.Flash();
            }
        }

        public void Initialize(CricketData data)
        {
            if (_initialized)
            {
                Debug.LogWarning("已经初始化过了");
                return;
            }
            print($"用{data.name}初始化SkillPanel");
            _initialized = true;
            _data = data;
            for (int i = 0; i < skillButtons.Count; i++)
            {
                skillButtons[i].Initialize(_data.skills[i]);
            }
        }
    }
}