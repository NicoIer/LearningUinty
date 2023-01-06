using System;
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


        private void Awake()
        {
            attack.onClick.AddListener(_attack_clicked);
            cricket.onClick.AddListener(_cricket_clicked);
            bag.onClick.AddListener(_bag_clicked);
            run.onClick.AddListener(_run_clicked);
        }
        
        private void _cricket_clicked()
        {
            print("点击Cricket按钮");
        }
        private void _bag_clicked()
        {
            print("点击bag按钮");
        }
        private void _attack_clicked()
        {
            print("点击战斗按钮");
        }

        private void _run_clicked()
        {
            print("点击逃跑按钮");

        }
    }
}