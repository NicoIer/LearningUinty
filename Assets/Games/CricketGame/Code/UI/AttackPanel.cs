using Games.CricketGame.Manager.Code.Pokemon;
using UnityEngine;

namespace Games.CricketGame.Manager.Code.UI
{
    public class AttackPanel : MonoBehaviour
    {
        private static readonly bool _debug = true;
        public CricketInfoPanel player;
        public CricketInfoPanel other;
        private bool _initialized = false;


        public void Initialize(CricketData p1, CricketData p2)
        {
            _initialized = true;
            player.Initialize(p1, false);
            other.Initialize(p2, true);
        }
    }
}