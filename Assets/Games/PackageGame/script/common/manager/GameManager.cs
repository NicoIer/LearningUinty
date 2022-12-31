using UnityEngine;

namespace PackageGame.Common.Manager 
{
    public class GameManager : Script.Tools.DesignPattern.Singleton<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();
            ResourcesManager.PreLoadResources();
        }

        public bool paused { get; private set; }

        public void Pause()
        {
            paused = true;
            Time.timeScale = 0;
            
        }

        public void Resume()
        {
            paused = false;
            Time.timeScale = 1;
        }
        
    }
}