using System;
using Cysharp.Threading.Tasks;
using Games.CricketGame.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.UI.Attack
{
    public class ConsolePanel : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = transform.GetChild(0).GetComponent<Text>();
        }

        public async UniTask UpdateText(string msg, int times)
        {
            _text.text = msg;
            for (int i = 0; i < times; i++)
            {
                if (GameManager.instance.input.enterDown)
                    return;
                await UniTask.WaitForFixedUpdate();
            }
        }
    }
}