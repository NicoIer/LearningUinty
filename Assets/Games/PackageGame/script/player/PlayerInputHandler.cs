using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PackageGame.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public PlayerInput input;
        public Vector2 move;
        public bool jump;
        
        public void OnMove(InputAction.CallbackContext ctx)
        {
            move = ctx.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {//started后立刻触发
                
            }else if (ctx.canceled)
            {//松开后触发
                jump = false;
            }
            else if (ctx.started)
            {//开始按下时触发
                jump = true;
            }
        }
    }
}