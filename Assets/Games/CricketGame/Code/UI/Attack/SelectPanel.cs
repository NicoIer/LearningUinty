using System;
using Games.CricketGame.Attack;
using Games.CricketGame.Code.Attack;
using Games.CricketGame.Cricket_;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public class SelectPanel : MonoBehaviour
    {
        public Button attack;
        public Button cricket;
        public Button bag;
        public Button run;
        public Action cricketClicked;
        public Action attackClicked;
        public Action bagClicked;
        public Action runClicked;


        private void Awake()
        {
            attack.onClick.AddListener(_attack_clicked);
            cricket.onClick.AddListener(_cricket_clicked);
            bag.onClick.AddListener(_bag_clicked);
            run.onClick.AddListener(_run_clicked);
        }
        
        private void _cricket_clicked()
        {
            cricketClicked?.Invoke();
            print("点击Cricket按钮");
        }
        private void _bag_clicked()
        {
            bagClicked?.Invoke();
            print("点击bag按钮");
        }
        private void _attack_clicked()
        {
            attackClicked?.Invoke();
            print("点击战斗按钮");
        }

        private void _run_clicked()
        {
            runClicked?.Invoke();
            InputStorage.player_input = new AttackInputStruct(SelectTypeEnum.逃跑);
        }
    }
}