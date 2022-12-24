using System;
using System.Collections;
using System.Collections.Generic;
using AttackGame.Data.Talking;
using Unity.VisualScripting;
using UnityEngine;

namespace AttackGame
{
    public class UIManager : MonoBehaviour
    {
        private GameObject _ui;
        public PackageManager packageManager;
        public TalkingManager talkingManager;
        public SimpleData data;

        public static UIManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                _ui = gameObject;
            }
        }

        private void Start()
        {
            _ui.SetActive(true); //不显示UI
            packageManager.SetActive(false); //不显示背包
            talkingManager.gameObject.SetActive(false); //不显示对话
        }

        private void Update()
        {
            //ToDo 其实没有按键时 完全不需要进行更新
            MenuControl();
        }

        private void MenuControl()
        {
            PackageUpdate();
            TalkingUpdate();
        }
        
        private void TalkingUpdate()
        {
            //Todo 仅限Debug使用 应该交给玩家控制器调用 对话显示
            if (Input.GetKeyDown(KeyCode.T))
            {
                //Debug.Log("按下T");
                if (!talkingManager.opened)
                {//没有打开对话则打开
                    talkingManager.Open(data);
                }
                else
                {
                    talkingManager.Close();
                }
            }
        }

        private void PackageUpdate()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                //假设按C打开背包
                //打开背包
                if (GameManager.instance.paused)
                {
                    packageManager.SetActive(false);
                    //继续游戏
                    GameManager.instance.Resume();
                }
                else
                {
                    packageManager.SetActive(true);
                    //暂停游戏
                    GameManager.instance.Pause();
                }
            }
        }
    }
}