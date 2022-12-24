using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AttackGame
{
    public class UIManager : Script.Tools.DesignPattern.Singleton<UIManager>
    {
        public GameObject ui;
        public PackageManager packageManager;

        private void Start()
        {
            ui.SetActive(false); //不显示UI
            packageManager.SetActive(false); //不显示背包
        }

        private void Update()
        {//ToDo 其实没有按键时 完全不需要进行更新
            MenuControl();
        }

        private void MenuControl()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {//假设按C打开背包
                //打开背包
                if (GameManager.instance.paused)
                {
                    ui.SetActive(false);
                    packageManager.SetActive(false);
                    //继续游戏
                    GameManager.instance.Resume();
                }
                else
                {
                    ui.SetActive(true);
                    packageManager.SetActive(true);
                    //暂停游戏
                    GameManager.instance.Pause();
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                if (!GameManager.instance.paused)
                {//游戏没有暂停的情况下 按T进行对话
                    
                    return;
                }
                else
                {//游戏暂停了 按 T没有用处
                    return;
                }
                
            }
        }
    }
}