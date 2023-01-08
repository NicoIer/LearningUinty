using System;
using System.Collections;
using Games.CricketGame.Code.Cricket_;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public class CricketAttackInfoCell : AbsCricketUI
    {
        #region UI Elemet

        public ImageBar healthBar;
        public ImageBar expBar;
        public Text nameText;
        public Text levelText;
        public Text healthText;

        #endregion
        

        public void Connect(CricketData data, bool isEnemy)
        {
            base.Connect(data);
            expBar.gameObject.SetActive(!isEnemy);
            healthText.gameObject.SetActive(!isEnemy);
            
            //刷新对应UI数值
            _exp_change(data.NeededExp(),data.LevelUpTotalExp());
            _health_change(data.healthAbility,data.defaultHealth);
            _level_change(data.level,data.level);
            _name_change(data.name,data.name);
        }
        

        protected override void _exp_change(float attained, float total)
        {
            expBar.fillAmount = attained / total;
        }

        protected override void _level_change(int pre, int now)
        {
            levelText.text = $"lv.{now}";
        }

        protected override void _name_change(string pre, string now)
        {
            nameText.text = now;
        }

        protected override void _meta_change(CricketData data)
        {
            
        }

        protected override void _health_change(float cur, float max)
        {
            healthText.text = $"{(int)cur}/{(int)max}";
            healthBar.fillAmount = cur / max;
        }
    }
}