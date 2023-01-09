using Games.CricketGame.Manager;
using UnityEngine;

namespace Nico.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 move => GameManager.instance.input.move;
        public short x => GameManager.instance.input.x;
        public short y => GameManager.instance.input.x;
        public bool jump => GameManager.instance.input.jump;
    }
}