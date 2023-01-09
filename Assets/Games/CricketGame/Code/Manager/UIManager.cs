using Games.CricketGame.Manager;
using Games.CricketGame.UI.Package;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance { get; private set; }
        [field: SerializeField] public AttackPanel attackPanel { get; private set; }
        [field: SerializeField] public CricketPanel cricketPanel { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public bool escDown => GameManager.instance.handler.escDown;
    }
}