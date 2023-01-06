using System;
using System.Collections;
using Games.CricketGame.Manager.Code.Manager;
using Games.CricketGame.Manager.Code.Pokemon;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.Manager.Code.UI
{
    public class CricketInfoPanel : MonoBehaviour
    {
        public ImageBar healthBar;
        public ImageBar expBar;
        public Text nameText;
        public Text levelText;
        public Text healthText;
        private bool _is_enemy;
        public bool expOver { get; private set; }
        public bool healthOver { get; private set; }
        public CricketData cricketData;


        public void Initialize(CricketData cricket_data, bool isEnemy)
        {
            expOver = true;
            _is_enemy = isEnemy;
            expBar.gameObject.SetActive(!isEnemy);
            healthText.gameObject.SetActive(!isEnemy);
            if (cricketData != null)
            {
                //清除之前注册的事件
                cricketData.damageAction -= _ApplyDamage;
                cricketData = null;
            }

            //绑定新的事件
            cricketData = cricket_data;
            cricketData.damageAction += _ApplyDamage;
            cricket_data.expAction += _AttainExp;
            //刷新对应UI数值
            FlashUI();
        }

        #region Update UI

        private void FlashUI()
        {
            _update_info();
            _update_exp();
            _update_health();
        }

        private void _update_exp()
        {
            if (_is_enemy)
                return;

            var attained = cricketData.LevelAttainedExp();
            var total = cricketData.LevelTotalExp();
            print($"刷新经验条,已经积累{attained},总共需要:{total}");
            expBar.fillAmount = (float)attained / total;
        }

        private void _update_health()
        {
            if (!_is_enemy)
            {
                healthText.text = $"{cricketData.healthAbility}/{cricketData.defalut_health}";
            }

            healthBar.fillAmount = (float)cricketData.healthAbility / cricketData.defalut_health;
        }

        private void _update_info()
        {
            nameText.text = $"{cricketData.name}";
            levelText.text = $"lv.{cricketData.level}";
        }

        #endregion

        #region IEnumerator

        /// <summary>
        /// 更新血条UI
        /// </summary>
        /// <param name="tempHealth"></param>
        /// <param name="maxHealth"></param>
        /// <param name="damage"></param>
        /// <param name="totalTimes"></param>
        /// <returns></returns>
        private IEnumerator _do_health_update(int damage, int totalTimes)
        {
            
            if (cricketData.healthAbility <= 0)
            {
                yield break;
            }

            healthOver = false;
            var _ = new WaitForFixedUpdate();
            //将伤害分成很多次进行UI刷新
            var one_damage = (float)damage / totalTimes;
            var maxHealth = cricketData.defalut_health; //最大值
            var tempHealth = (float)cricketData.healthAbility; //临时值
            for (var i = 1; i != totalTimes + 1; i++)
            {
                tempHealth -= i * one_damage;
                healthBar.fillAmount = tempHealth / maxHealth;
                healthText.text = $"{(int)tempHealth}/{maxHealth}";
                if (tempHealth <= 0)
                {
                    healthOver = true;
                    cricketData.healthAbility -= damage;
                    cricketData.healthAbility = 0;
                    yield break;
                }
                if (tempHealth >= maxHealth)
                {
                    cricketData.healthAbility = maxHealth;
                }

                yield return _;
            }

            cricketData.healthAbility -= damage;
            healthOver = true;
        }

        private void _ApplyDamage(int damage)
        {
            if (cricketData == null)
            {
                Debug.LogWarning("未绑定cricketInfoPanel对应的cricket数据");
                return;
            }

            StartCoroutine(_do_health_update(damage, 30));
        }


        private IEnumerator _do_exp_update(int exp, int totalTimes)
        {
            expOver = false;
            var _ = new WaitForFixedUpdate();
            var needed = cricketData.NeededExp(); //升级所需经验值
            var total = cricketData.LevelTotalExp(); //从0升到下一级需要的经验值
            float cur_exp;
            float one_exp;
            
            while (exp >= needed)
            {
                exp -= needed;
                one_exp = (float)needed / totalTimes; //从需要的部分开始增加
                //进行血条更新 然后使 对应精灵升级
                cur_exp = cricketData.LevelAttainedExp(); //获取当前等级下 已经积累的经验值
                for (var i = 1; i != totalTimes; i++)
                {
                    cur_exp += one_exp;
                    expBar.fillAmount = cur_exp / total;
                    yield return _;
                }

                cricketData.alreadyExperience += needed;
                cricketData.LevelUp(); //升级
                _update_info();
                _update_health();
                if (cricketData.level == 100)
                {
                    //满级了就不要再搞了
                    expBar.fillAmount = 1;
                    expOver = true;
                    yield break;
                }

                //更新下一级需要的经验
                needed = cricketData.NeededExp();
                total = cricketData.LevelTotalExp();
            } //升级升完了剩下的经验不够升级时

            if (cricketData.level != 100)
            {
                cur_exp = cricketData.LevelAttainedExp(); //获取当前等级下 已经积累的经验值
                one_exp = (float)exp / totalTimes;
                for (var i = 1; i != totalTimes; i++)
                {
                    cur_exp += one_exp;
                    expBar.fillAmount = cur_exp / total;
                    yield return _;
                }
            }


            cricketData.alreadyExperience += exp;
            expOver = true;
        }

        private void _AttainExp(int exp)
        {
            if (_is_enemy || cricketData == null)
            {
                Debug.LogWarning("在对null/enemy Cricket 更新经验条!!");
                return;
            }

            if (exp <= 0)
            {
                Debug.LogWarning($"{cricketData.name}获得的经验<=0!!!");
                return;
            }

            if (cricketData.level == 100)
            {
                expBar.fillAmount = 1;
                return;
            }
            StartCoroutine(_do_exp_update(exp, 30));
        }

        #endregion
    }
}