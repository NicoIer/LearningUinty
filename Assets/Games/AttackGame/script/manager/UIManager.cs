using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AttackGame
{
    public class UIManager : Script.Tools.DesignPattern.Singleton<UIManager>
    {
        public GameObject menu;
        public PackageManager packageManager;
        private void Start()
        {
            menu.SetActive(false);
        }

        private void Update()
        {
            MenuControl();
        }

        private void MenuControl()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameManager.instance.paused)
                {
                    //继续
                    Resume();
                }
                else
                {
                    //暂停
                    Pause();
                }
            }
        }

        private void Resume()
        {
            menu.SetActive(false);
            Time.timeScale = 1;
            GameManager.instance.paused = false;
        }

        private void Pause()
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            GameManager.instance.paused = true;
        }
    }
}