using Games.CricketGame.UI.Package;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public class CricketPanel : MonoBehaviour
    {
        [field: SerializeField] public CricketSelectPanel cricketSelectPanel { get; private set; }
        private bool _activated;

        public void Open()
        {
            if (_activated) return;
            gameObject.SetActive(true);
            _activated = true;
        }

        public void OpenSelectPanel()
        {
            Open();
            cricketSelectPanel.gameObject.SetActive(true);
        }

        public void CloseSelectPanel()
        {
            cricketSelectPanel.gameObject.SetActive(false);
        }
    }
}