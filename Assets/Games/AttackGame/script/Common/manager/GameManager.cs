using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AttackGame.Common.Manager 
{
    public class GameManager : Script.Tools.DesignPattern.Singleton<GameManager>
    {
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