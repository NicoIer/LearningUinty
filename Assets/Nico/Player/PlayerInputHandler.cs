using Games.CricketGame.Manager;
using UnityEngine;

namespace Nico.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 move => GameManager.instance.handler.move;
        public short x => GameManager.instance.handler.x;
        public short y => GameManager.instance.handler.x;
        public bool jump => GameManager.instance.handler.jump;
    }
}