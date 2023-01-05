using Games.CricketGame.Manager.Code.Pokemon;
using UnityEngine;

namespace Games.CricketGame.Manager.Code.UI
{
    /// <summary>
    /// 战斗管理 
    /// </summary>
    public class AttackManager : MonoBehaviour
    {
        private Cricket p1;
        private Cricket p2;
        public AttackPanel ui;



        public void EnterAttack(Cricket p1, Cricket p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            ui.Initialize(p1.data, p2.data);
        }
    }
}