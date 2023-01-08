using Games.CricketGame.Cricket_;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public class SkillBagCell : AbsSkillUI
    {
        public Text nameText;
        public PropertyIcon propertyIcon;
        public Text ppText;

        protected override void _on_clicked()
        {
            print($"{name}被点击了");
        }

        protected override void _effect_change(EffectEnum effectEnum)
        {
            
        }

        public override void Connect(Skill skill)
        {
            base.Connect(skill);
            _name_change(skill.meta.name);
            _pp_change(skill.cur_times,skill.use_times);
        }

        public void Clear()
        {
            
        }

        protected override void _meta_change(Skill skill)
        {
            
        }

        protected override void _name_change(string now)
        {
            nameText.text = now;
        }

        protected override void _property_change(PropertyEnum now)
        {
            
        }

        protected override void _pp_change(int cur,int max)
        {
            ppText.text = $"{cur}/{max}";
        }
    }
}