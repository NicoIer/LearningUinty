using AttackGame.Common.Manager;
using AttackGame.Package;
using AttackGame.Talking;
using UnityEngine;

namespace AttackGame.UI
{
    public class UIManager : MonoBehaviour
    {
        private GameObject _ui;
        public PackageManager packageManager;
        public TalkingManager talkingManager;
        public static UIManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                _ui = gameObject;
                _ui.gameObject.SetActive(true);
                packageManager.gameObject.SetActive(true);
                packageManager.gameObject.SetActive(false);
                
                talkingManager.gameObject.SetActive(true);
                talkingManager.gameObject.SetActive(false);

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
        }

        public void OpenTalkingPanel(TalkingData data)
        {
            if(!talkingManager.opened)
                talkingManager.Open(data);
        }

        public void CloseTalkingPanel()
        {
            talkingManager.Close();
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