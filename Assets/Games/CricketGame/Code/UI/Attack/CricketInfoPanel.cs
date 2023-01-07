using System;
using System.Collections;
using Games.CricketGame.Code.Cricket_;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public class CricketInfoPanel : MonoBehaviour
    {
        #region UI Elemet

        public ImageBar healthBar;
        public ImageBar expBar;
        public Text nameText;
        public Text levelText;
        public Text healthText;

        #endregion

        private CricketData _bandedData;


        public void Connect(CricketData cricket_data, bool isEnemy)
        {
            expBar.gameObject.SetActive(!isEnemy);
            healthText.gameObject.SetActive(!isEnemy);

            _bandedData = cricket_data;
            _bandedData.healthRateChangeAction += _update_health;
            _bandedData.expRateChangeAction += _update_exp;
            _bandedData.levelUpAction += _update_info;
            //刷新对应UI数值
            _update_info(_bandedData.name, _bandedData.level);
        }

        public void DisConnect()
        {
            _bandedData.healthRateChangeAction -= _update_health;
            _bandedData.expRateChangeAction -= _update_exp;
            _bandedData.levelUpAction -= _update_info;
            _bandedData = null;
        }
        
        #region Update UI Method

        private void _update_info(string cricketName, int level)
        {
            nameText.text = $"{cricketName}";
            levelText.text = $"lv.{level}";
        }

        private void _update_exp(float attained, float total)
        {
            expBar.fillAmount = attained / total;
        }





        private void _update_health(float now, float max)
        {
            healthText.text = $"{(int)now}/{(int)max}";
            healthBar.fillAmount = now / max;
        }

        #endregion
    }
}