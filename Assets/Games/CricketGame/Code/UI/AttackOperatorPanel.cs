using System;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.Manager.Code.UI
{
    public class AttackOperatorPanel : MonoBehaviour
    {
        public Button attackBtn;
        public Button cricketBtn;
        public Button bagBtn;
        public Button runBtn;

        private void Awake()
        {
            attackBtn.onClick.AddListener(_attack_clicked);
            runBtn.onClick.AddListener(_run_clicked);
            cricketBtn.onClick.AddListener(_cricket_clicked);
            bagBtn.onClick.AddListener(_bag_clicked);
        }

        public void Initialize()
        {
            
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