using System;
using Games.CricketGame.Cricket_;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public abstract class AbsSkillUI: MonoBehaviour
    {
        private Button _button;
        protected Skill _skill;
        protected bool _connected;
        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_on_clicked);
        }

        protected abstract void _on_clicked();
        protected abstract void _property_change(PropertyEnum propertyEnum);
        protected abstract void _pp_change(int cur, int max);
        protected abstract void _name_change(string name);
        protected abstract void _meta_change(Skill skill);
        protected abstract void _effect_change(EffectEnum effectEnum);
        public virtual void Connect(Skill skill)
        {
            if (_connected)
            {
                DisConnect();
            }
            _connected = true;
            _skill = skill;
            skill.ppChangeAction += _pp_change;
            skill.nameChangeAction += _name_change;
            skill.properyuChangeAction += _property_change;
            skill.metaChangeAction += _meta_change;
            skill.effectChangeAction += _effect_change;
        }

        public virtual void DisConnect()
        {
            if (!_connected)
                return;
            _skill.ppChangeAction -= _pp_change;
            _skill.nameChangeAction -= _name_change;
            _skill.properyuChangeAction -= _property_change;
            _skill.metaChangeAction -= _meta_change;
            _skill.effectChangeAction -= _effect_change;
            
            _skill = null;
            _connected = false;
        }
        
        
        

    }
}