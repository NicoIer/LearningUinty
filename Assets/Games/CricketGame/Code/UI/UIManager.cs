using Games.CricketGame.Manager;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance { get; private set; }

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