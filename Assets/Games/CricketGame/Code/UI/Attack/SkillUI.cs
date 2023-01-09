using Games.CricketGame.Attack;
using Games.CricketGame.Cricket_;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public class SkillUI : AbsSkillUI
    {
        public Text nameText;
        public Text effectText;
        public Image propertyImage;
        public Text ppText;


        protected override void  Awake()
        {
            base.Awake();
            if (nameText == null)
            {
                nameText = transform.Find("nameText").GetComponent<Text>();
            }
        }

        protected override void _on_clicked()
        {
            if (!_connected)
            {
                Debug.LogError("点击未初始化的技能按钮");
            }

            print($"选择使用{_skill.meta.name}");
            AttackInputHandler.player_input = _skill;
        }

        public override void Connect(Skill skill)
        {
            base.Connect(skill);
            //ToDo 完成图标和效果的更新
            _name_change(_skill.meta.name);
            _pp_change(_skill.cur_times, _skill.use_times);
            _property_change(_skill.propertyEnum);
            _effect_change(skill.effectEnum);
        }
        
        protected override void _meta_change(Skill skill)
        {
            
        }

        protected override void _name_change(string now)
        {
            nameText.text = now;
        }

        protected override void _pp_change(int cur, int max)
        {
            ppText.text = $"{cur}/{max}";
        }

        protected override void _property_change(PropertyEnum now)
        {//ToDo Fix it
        }

        protected override void _effect_change(EffectEnum effectEnum)
        {
            effectText.text = effectEnum.ToString();
        }
    }
}