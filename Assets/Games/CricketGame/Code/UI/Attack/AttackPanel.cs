using Games.CricketGame.Code.Cricket_;
using UnityEngine;

namespace Games.CricketGame.UI
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
            player.Connect(p1, false);
            other.Connect(p2, true);
            //初始化操作面板
            attackOperatorPanel.Initialize(p1);
        }

        public void RoundStart()
        {
            if (!_initialized)
            {
                Debug.LogError("AttackPanel未初始化");
                return;
            }
            gameObject.SetActive(true);
            attackOperatorPanel.RoundStart();

        }


    }
}