using System;
using Games.CricketGame.Code.UI.Attack;
using Games.CricketGame.Cricket_;
using Games.CricketGame.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public class SkillButton : MonoBehaviour
    {
        public Button button;
        public Text nameText;
        public Text effectText;
        public Image propertyImage;
        public Text ppText;
        private bool _initialized;
        private Skill _skill;

        private void Awake()
        {
            if (nameText == null)
            {
                nameText = transform.Find("nameText").GetComponent<Text>();
            }

            if (button == null)
            {
                button = GetComponent<Button>();
            }
            button.onClick.AddListener(_clicked);
        }

        public void Initialize(Skill skill)
        {
            print($"SkillButton初始化{skill.meta.name}");
            _initialized = true;
            _skill = skill;
            //ToDo 这里只做测试
            nameText.text = skill.meta.name;
            ppText.text = $"{skill.cur_times}/{skill.use_times}";
            // propertyImage.sprite = sprite;
            // effectText.text = effect;
        }

        public void Flash()
        {
            if(!_initialized)
                return;
            ppText.text = $"{_skill.cur_times}/{_skill.use_times}";
        }
        private void _clicked()
        {
            if (!_initialized)
            {
                Debug.LogError("点击未初始化的技能按钮");
            }
            print($"选择使用{_skill.meta.name}");
            AttackInputHandler.player_input = _skill;
        }
        
    }
}