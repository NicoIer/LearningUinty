﻿using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Games.CricketGame.Manager
{
    public class GameInputHandler: MonoBehaviour
    {
        public bool escDown;
        public Vector2 move;
        public short x;
        public short y;
        public bool jump;

        public void OnEscDown(InputAction.CallbackContext obj)
        {
            print("escdown");
            if (obj.performed)
            {
                escDown = true;
            }
            else if (obj.canceled)
            {
                escDown = false;
            }
        }


        public void OnMove(InputAction.CallbackContext ctx)
        {
            move = ctx.ReadValue<Vector2>();
            x = (short)move.x;
            y = (short)move.y;
        }

        private async UniTask _set_jump(bool value)
        {
            await UniTask.WaitForFixedUpdate();
            jump = value;
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                //started后立刻触发 这里是按下案件0.02s后触发
                _set_jump(false).Forget();
            }
            else if (ctx.canceled)
            {
                //松开后触发
                jump = false;
            }
            else if (ctx.started)
            {
                //开始按下时触发
                jump = true;
            }
        }
    }
}