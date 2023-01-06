using Games.CricketGame.Manager.Code.Pokemon;
using UnityEngine;

namespace Games.CricketGame.Manager.Code.UI
{
    public class AttackPanel : MonoBehaviour
    {
        private static readonly bool _debug = true;
        public CricketInfoPanel player;
        public CricketInfoPanel other;
        public AttackOperatorPanel attackOperatorPanel;
        private bool _initialized;


        public void Initialize(CricketData p1, CricketData p2)
        {
            _initialized = true;
            gameObject.SetActive(true);
            //将自身/对方的Cricket数据传递给panel
            player.Initialize(p1, false);
            other.Initialize(p2, true);
            
        }
    }
}