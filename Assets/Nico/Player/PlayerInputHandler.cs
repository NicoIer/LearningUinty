using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nico.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 move;
        public short x;
        public short y;
        public bool jump;
        
        public void OnMove(InputAction.CallbackContext ctx)
        {
            move = ctx.ReadValue<Vector2>();
            x = (short)move.x;
            y = (short)move.y;
        }

        private IEnumerator _set_jump(bool value,float delay)
        {
            yield return new WaitForSeconds(delay);
            jump = value;
        }
        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {//started后立刻触发 这里是按下案件0.02s后触发
                StartCoroutine(_set_jump(false,0.02f));
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