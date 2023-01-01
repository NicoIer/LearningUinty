using UnityEngine;
using UnityEngine.Serialization;

namespace Games.CricketGame.Code.Player
{
    [CreateAssetMenu(fileName = "PlayerPhysicsData", menuName = "Data/CricketGame/Player/PlayerPhysicsData", order = 0)]
    public class PlayerPhysicsData : ScriptableObject
    {
        public float xSpeed;
        public float ySpeed;
        public float jumpSpeed;
    }
}