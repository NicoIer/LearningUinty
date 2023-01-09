using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Games.CricketGame.Manager
{
    public class GameInputHandler : MonoBehaviour
    {
        [field: SerializeField] public bool escDown { get; private set; }
        [field: SerializeField] public bool enterDown { get; private set; }
        [field: SerializeField] public Vector2 move { get; private set; }
        [field: SerializeField] public short x { get; private set; }
        [field: SerializeField] public short y { get; private set; }
        [field: SerializeField] public bool jump { get; private set; }

        public void OnEscDown(InputAction.CallbackContext obj)
        {
            if (obj.performed)
            {
                escDown = true;
            }
            else if (obj.canceled)
            {
                escDown = false;
            }
        }

        public void onEnterDown(InputAction.CallbackContext obj)
        {
            if (obj.performed)
            {
                enterDown = true;
            }
            else if (obj.canceled)
            {
                enterDown = false;
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