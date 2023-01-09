using Games.CricketGame.Manager;
using Games.CricketGame.Player_;
using Games.CricketGame.UI.Package;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public class CricketPanel : MonoBehaviour
    {
        [field: SerializeField] public CricketSelectPanel cricketSelectPanel { get; private set; }
        private bool _activated;
        private bool _connected;
        private Player _player;
        public void Open()
        {
            if (_activated) return;
            gameObject.SetActive(true);
            _activated = true;
        }

        public void Connect(Player player)
        {
            cricketSelectPanel.gameObject.SetActive(true);
            cricketSelectPanel.gameObject.SetActive(false);
            if (_connected)
            {
                DisConnect();
            }

            this._player = player;
            cricketSelectPanel.Connect(player);
            _connected = true;
        }

        public void DisConnect()
        {
            if (!_connected)
            {
                return;
            }

            this._player = null;
            cricketSelectPanel.DisConnect();
            _connected = false;
        }
        public void OpenSelectPanel()
        {
            if (!_connected)
            {
                Debug.LogError("未链接到玩家的CricketPanel!!!");
            }
            Open();
            cricketSelectPanel.gameObject.SetActive(true);
        }

        public void CloseSelectPanel()
        {
            cricketSelectPanel.gameObject.SetActive(false);
        }
    }
}