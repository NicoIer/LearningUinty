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
        private bool _connected;
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

        public void Connect(Skill skill)
        {
            _connected = true;
            _skill = skill;
            _skill.ppChangeAction += _pp_change;
            _skill.nameChangeAction += _name_change;
            _skill.properyuChange += _property_change;
            //ToDo 完成图标和效果的更新
            _name_change(_skill.meta.name);
            _pp_change(_skill.cur_times, _skill.use_times);
            // propertyImage.sprite = sprite;
            // effectText.text = effect;
        }

        public void DisConnect()
        {
            print("skillButton断开");
            _connected = false;
            _skill.ppChangeAction -= _pp_change;
            _skill.nameChangeAction -= _name_change;
            _skill.properyuChange -= _property_change;
            _skill = null;
        }

        private void _name_change(string now)
        {
            nameText.text = now;
        }

        private void _pp_change(int cur, int max)
        {
            ppText.text = $"{_skill.cur_times}/{_skill.use_times}";
        }

        private void _property_change(PropertyEnum now)
        {
        }

        private void _effect_change()
        {
        }

        private void _clicked()
        {
            if (!_connected)
            {
                Debug.LogError("点击未初始化的技能按钮");
            }

            print($"选择使用{_skill.meta.name}");
            AttackInputHandler.player_input = _skill;
        }
    }
}