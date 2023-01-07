using Games.CricketGame.Code.Cricket_;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public class AttackPanel : MonoBehaviour
    {
        public CricketInfoPanel player;
        public CricketInfoPanel other;
        public AttackOperatorPanel attackOperatorPanel;
        private bool _connected;


        public void Connect(CricketData p1, CricketData p2)
        {
            _connected = true;
            //将自身/对方的Cricket数据传递给panel
            player.gameObject.SetActive(true);
            other.gameObject.SetActive(true);
            player.Connect(p1, false);
            other.Connect(p2, true);
            //初始化操作面板
            attackOperatorPanel.gameObject.SetActive(true);
            attackOperatorPanel.Connect(p1);
        }

        public void DisConnect()
        {
            _connected = false;
            player.DisConnect();
            other.DisConnect();
            attackOperatorPanel.DisConnect();
            player.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }

        public void RoundStart()
        {
            //显示UI
            if (!_check())
                return;
            gameObject.SetActive(true);
            attackOperatorPanel.gameObject.SetActive(true);
            attackOperatorPanel.RoundStart();
        }

        public void RoundPlaying()
        {
            if (!_check())
                return;
            print("隐藏操作面板");
            attackOperatorPanel.gameObject.SetActive(false);
            print("显示事件面板");
            //ToDo Fix it
        }

        private bool _check()
        {
            if (!_connected)
            {
                Debug.LogError("AttackPanel未初始化");
                return false;
            }

            return true;
        }
    }
}