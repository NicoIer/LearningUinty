using System;
using System.Collections.Generic;
using Games.CricketGame.Code.Cricket_;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public class SkillPanel : MonoBehaviour
    {
        public List<SkillButton> skillButtons;
        private bool _banded;
        private CricketData _data;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var skillButton = transform.GetChild(i).GetComponent<SkillButton>();
                skillButtons.Add(skillButton);
            }
        }

        public void Band(CricketData data)
        {
            _banded = true;
            _data = data;
            for (int i = 0; i < skillButtons.Count; i++)
            {
                skillButtons[i].Band(data.skills[i]);
            }
        }
    }
}